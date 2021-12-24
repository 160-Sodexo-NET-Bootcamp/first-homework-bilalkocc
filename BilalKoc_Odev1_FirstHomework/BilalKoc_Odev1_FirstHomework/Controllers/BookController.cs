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
        public Book(int id, int kitapserino, string kitapadi, string yazaradi, DateTime basimyili)
        {
            this.Id = id;
            this.KitapSeriNo = kitapserino;
            this.KitapAdi = kitapadi;
            this.YazarAdi = yazaradi;
            this.BasimYili = basimyili;
        }

    }

    [Route("api/[controller]s")]//Tek controller üzerinden işlem yaptığımız için api/ yapmasak olurdu. Ancak düzenli bir ağaç yapısı için yapılması önemli. 
    [ApiController]
    public class BookController : ControllerBase
    {

        static List<Book> books = new List<Book>();
        public BookController()
        {
            if (books.Count == 0)
            {
                books.Add(new Book(1, 101, "kürk mantolu madonna", "Sabahattin Ali", new DateTime(1940, 12, 18)));
                books.Add(new Book(2, 102, "iki şehrin hikayesi", "Charles Dickens", new DateTime(1859, 12, 18)));
                books.Add(new Book(3, 103, "küçük prens", "Antoine", new DateTime(1943, 12, 18)));
                books.Add(new Book(4, 104, "İnsancıklar", "Dostoyevski", new DateTime(1846, 12, 18)));
                books.Add(new Book(5, 105, "Kumarbaz", "Dostoyevski", new DateTime(1866, 12, 18)));
                books.Add(new Book(6, 106, "Taş Meclisi", "Grange", new DateTime(2000, 12, 18)));
            }
        }

       
        [HttpPost]//Liste içerisindeki tüm kitapları özellikleri ile birlikte geri döndürür.
        public List<Book> GetBooks()
        {
            return books;
        }

        [HttpGet("{id}")]//Parametre olarak aldığı id'ye ait kitabın tüm özelliklerini geri döner.Bunu FromRoute ile yapar.URL'de api/books/3 örneği gibi görünür.
        public Book GetBookFromRoute([FromRoute] int id)
        {
            return books.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpGet]//Parametre olarak aldığı id'ye ait kitabın tüm özelliklerini geri döner.Bunu FromQuery ile yapar.URL'de api/books?id=3 örneği gibi görünür.
        public Book GetBookFromQuery([FromQuery] int id)
        {
            return books.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost("insertbook")]//book veri tipinde aldığı bir json verisini listenin içine yazar.
        public ActionResult InsertBook([FromBody] Book book)
        {
            books.Add(book);
            return Ok();
        }
        

        [HttpPut]//Güncelle işlemi yapar.
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

        [HttpDelete]//Parametre olarak aldığı id'ye ait kitabın bilgilerini siler.
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
