using MediatR;
using TransactionsApi.Interface;
using TransactionsApi.Models;

namespace TransactionsApi.CQRS.Commands.WriteOff
{
    public class WriteOffCommandHandler : IRequestHandler<WriteOffCommand, WriteOffCommandResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public WriteOffCommandHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<WriteOffCommandResult> Handle(WriteOffCommand request,
            CancellationToken cancellationToken)
        {
            if (!_clientRepository.ClientExists(request.ClientId))
            {
                throw new InvalidOperationException("Unable to perform write off. Client not found.");
            }

            var currentBalance = await _transactionRepository.
                GetClientCurrentBalanceAsync(request.ClientId);

            if ((currentBalance - request.Amount) < 0)
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

            return new WriteOffCommandResult { Message = "Successfull write off." };
        }
    }
}
