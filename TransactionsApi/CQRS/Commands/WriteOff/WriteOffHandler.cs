using MediatR;
using TransactionsApi.Interface;
using TransactionsApi.Models;

namespace TransactionsApi.CQRS.Commands.WriteOff
{
    public class WriteOffHandler : IRequestHandler<WriteOffCommand, WriteOffResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public WriteOffHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<WriteOffResult> Handle(WriteOffCommand request,
            CancellationToken cancellationToken)
        {
            if (!_clientRepository.ClientExists(request.ClientId))
            {
                throw new InvalidOperationException("Unable to perform write off. Client not found.");
            }

            var transactions = _transactionRepository.GetTransactionsByClientIdAsync(request.ClientId);
            decimal currentBalance = 0;
            foreach (var transaction in await transactions)
            {
                currentBalance += transaction.Amount;
            }

            if (currentBalance - request.Amount < 0)
            {
                throw new InvalidOperationException
                    ("Unable to perform write off. Insufficient funds in the account.");
            }

            var writeOff = new Transaction
            {
                Amount = -(request.Amount),
                ClientId = request.ClientId
            };

            await _transactionRepository.CreateTransactionAsync(writeOff);

            return new WriteOffResult { Message = "Successfull write off." };
        }
    }
}
