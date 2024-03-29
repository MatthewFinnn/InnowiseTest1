//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected string title;      // Данные, содержащиеся в любом объекте класса Man
        protected int id_Book;
        protected DateTime year;

        static int maxTitle = 30;
        public Book()
        {
            this.BookAuthor = new HashSet<BookAuthor>();
        }
    
        public int Id_Book { get; set; }
        public string Title {
            get { return title; }
            set
            {
                if (value.Length > maxTitle)
                    title = value.Substring(0, maxTitle);
                else
                    title = value;
            }
        }
        public System.DateTime Year {
            get { return year; }
            set
            {
                if (value.Year > DateTime.Now.Year)
                    year = DateTime.Now;
                else
                    year = value;
            }
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookAuthor> BookAuthor { get; set; }
    }
}
