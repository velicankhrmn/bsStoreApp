using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Models.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book {Id = 1, Title = "Mesneviden Hikayeler",Price = 66},
                new Book {Id = 2, Title = "İnsan ne ile yaşar ?", Price = 266},
                new Book {Id = 3, Title = "Suç ve Ceza", Price = 166}
            );
        }
    }
}
