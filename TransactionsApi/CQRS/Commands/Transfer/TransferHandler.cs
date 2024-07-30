using MediatR;
using TransactionsApi.Interface;
using TransactionsApi.Models;

namespace TransactionsApi.CQRS.Commands.Transfer
{
    public class TransferHandler : IRequestHandler<TransferCommand, TransferResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public TransferHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<TransferResult> Handle(TransferCommand request,
            CancellationToken cancellationToken)
        {
            if (!_clientRepository.ClientExists(request.SenderId))
            {
                throw new InvalidOperationException("Unable to perform transfer. Sender not found.");
            }

            if (!_clientRepository.ClientExists(request.RecieverId))
            {
                throw new InvalidOperationException("Unable to perform transfer. Reciever not found.");
            }

            var transactions = _transactionRepository.GetTransactionsByClientIdAsync(request.SenderId);
            decimal senderBalance = 0;
            foreach (var transaction in await transactions)
            {
                senderBalance += transaction.Amount;
            }

            if (senderBalance - request.Amount < 0)
            {
                throw new InvalidOperationException
                    ("Unable to perform write off. Insufficient funds in the sender account.");
            }

            var writeOff = new Transaction
            {
                Amount = -(request.Amount),
                ClientId = request.SenderId
            };
            await _transactionRepository.CreateTransactionAsync(writeOff);

            var accrual = new Transaction
            {
                Amount = request.Amount,
                ClientId = request.RecieverId
            };
            await _transactionRepository.CreateTransactionAsync(accrual);

            return new TransferResult { Message = "Successfull transfer." };
        }
    }
}
