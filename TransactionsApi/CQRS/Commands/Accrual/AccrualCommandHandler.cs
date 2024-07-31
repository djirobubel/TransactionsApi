using MediatR;
using TransactionsApi.Interface;
using TransactionsApi.Models;

namespace TransactionsApi.CQRS.Commands.Accrual
{
    public class AccrualCommandHandler : IRequestHandler<AccrualCommand, AccrualCommandResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public AccrualCommandHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<AccrualCommandResult> Handle(AccrualCommand request,
            CancellationToken cancellationToken)
        {
            if(!_clientRepository.ClientExists(request.ClientId))
            {
                throw new InvalidOperationException("Unable to perform accrual. Client not found.");
            }

            var accrual = new Transaction
            {
                Amount = request.Amount,
                ClientId = request.ClientId
            };

            await _transactionRepository.CreateTransactionAsync(accrual);

            return new AccrualCommandResult { Message = "Successfull accrual." };
        }
    }
}
