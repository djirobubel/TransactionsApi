using MediatR;

namespace TransactionsApi.CQRS.Queries.GetClientTransactions
{
    public class GetClientTransactionsQuery : IRequest<GetClientTransactionsResult>
    {
        public Guid ClientId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }

        public GetClientTransactionsQuery(Guid clientId, int pageNumber, int pageSize, string sortBy,
            bool isAscending)
        {
            ClientId = clientId;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortBy = sortBy;
            IsAscending = isAscending;
        }
    }
}