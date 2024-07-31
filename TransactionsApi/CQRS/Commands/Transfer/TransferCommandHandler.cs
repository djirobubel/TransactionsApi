using MediatR;
using TransactionsApi.Interface;
using TransactionsApi.Models;

namespace TransactionsApi.CQRS.Commands.Transfer
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, TransferCommandResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public TransferCommandHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<TransferCommandResult> Handle(TransferCommand request,
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

            decimal senderBalance = await _transactionRepository.
                GetClientCurrentBalanceAsync(request.SenderId);

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

            return new TransferCommandResult { Message = "Successfull transfer." };
        }
    }
}
