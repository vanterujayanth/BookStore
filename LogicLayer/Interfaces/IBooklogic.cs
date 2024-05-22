using modelLayer;
using RepostoryLayer.Entity;

namespace LogicLayer.Interfaces
{
    public interface IBooklogic
    {
        public BookModel Add(BookModel model);
        public List<BookEntity> GetAllBooks();
        public object EmployeeUpdate(int noteid,BookModel model);
        //public BookModel EmployeeUpdate(BookModel model);
        public BookEntity GetBookById(int id);
        public bool DeleteBook(int id);
    }
}