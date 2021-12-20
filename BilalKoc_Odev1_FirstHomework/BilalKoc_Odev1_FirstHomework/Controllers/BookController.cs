using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilalKoc_Odev1_FirstHomework.Controllers
{
    public class Book
    {
        public int Id { get; set; }
        public int KitapSeriNo { get; set; }
        public string KitapAdi { get; set; }
        public string YazarAdi { get; set; }
        public DateTime BasimYili { get; set; }
        public Book(int id,int kitapserino,string kitapadi,string yazaradi,DateTime basimyili)
        {
            this.Id = id;
            this.KitapSeriNo = kitapserino;
            this.KitapAdi = kitapadi;
            this.YazarAdi = yazaradi;
            this.BasimYili = basimyili;
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        static List<Book> books = new List<Book>();
        public BookController()
        {
            if (books.Count == 0)
            {
                books.Add(new Book(1, 101, "kürk mantolu madonna", "Sabahattin Ali", new DateTime(1940, 12, 18)));
                books.Add(new Book(2, 102, "iki şehrin hikayesi", "charles dickens", new DateTime(1941, 12, 18)));
                books.Add(new Book(3, 103, "küçük prens", "antoine", new DateTime(1942, 12, 18)));
                books.Add(new Book(4, 104, "anna karenina 1", "lev tolstoy", new DateTime(1943, 12, 18)));
                books.Add(new Book(5, 105, "anna karenina 2", "lev tolstoy", new DateTime(1944, 12, 18)));
                books.Add(new Book(6, 106, "anna karenina 3", "lev tolstoy", new DateTime(1945, 12, 18)));
            }
        }

       
        [HttpPost]
        public List<Book> GetBooks()
        {
            return books;
        }

        [HttpGet("{id}")]
        public Book GetBookFromRoute([FromRoute] int id)
        {
            return books.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpGet]
        public Book GetBookFromQuery([FromQuery] int id)
        {
            return books.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost("insertbook")]
        public ActionResult InsertBook([FromBody] Book book)
        {
            books.Add(book);
            return Ok();
        }
        

        [HttpPut]
        public ActionResult UpdateBook([FromBody] Book book)
        {
            Book temp = books.Where(x => x.Id == book.Id).FirstOrDefault();
            if (temp == null)
            {
                return BadRequest();
            }
            temp.KitapAdi = book.KitapAdi;
            temp.KitapSeriNo = book.KitapSeriNo;
            temp.YazarAdi = book.YazarAdi;
            temp.BasimYili = book.BasimYili;

            return Ok(temp);
        }

        [HttpDelete]
        public ActionResult DeleteBook([FromQuery] int id)
        {
            Book temp = books.Where(x => x.Id == id).FirstOrDefault();
            if (temp == null)
            {
                return BadRequest();
            }
            books.Remove(temp);

            return Ok();
        }


    }
}
