namespace Feedback.Dtos
{
    public class Pagination
    {
        const int _MaxPage = 50;
        private int _PageSize = 7;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = (value > _MaxPage) ? _MaxPage : value;
            }
        }
    }
}
