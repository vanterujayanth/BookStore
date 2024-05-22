using LogicLayer.Interfaces;
using modelLayer;
using RepostoryLayer.Entity;
using RepostoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services
{
    public class Booklogic:IBooklogic
    {
        private readonly IBookRepo bookRepo;
        public Booklogic(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        public BookModel Add(BookModel model)
        {
            return bookRepo.Add(model);
        }
        public List<BookEntity> GetAllBooks()
        {
            return bookRepo.GetAllBooks();
        }
        public object EmployeeUpdate(int noteid,BookModel model)
       // public BookModel EmployeeUpdate(BookModel model)
        {
            
           return bookRepo.EmployeeUpdate(noteid,model);
        }
        public BookEntity GetBookById(int id)
        {
            return bookRepo.GetBookById(id);
        }
        public bool DeleteBook(int id)
        {
            return bookRepo.DeleteBook(id);
        }
    }
}
