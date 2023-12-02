using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQDockerEntityExample.DAL;

public class Author
{
    public long Id { get; set; } // Primary key
    public string Name { get; set; } // Unique index
    public virtual Image Image { get; set; } // One-To-One
}
public class Article
{
    public long Id { get; set; } // Primary key
    public string Title { get; set; } // Index
    public virtual ICollection<Author> Author { get; set; } // Many-To-Many
    public virtual Site Site { get; set; } // One-To-Many
}
public class Site
{
    public long Id { get; set; } // Primary key
    public DateTimeOffset CreatedAt { get; set; }
}
public class Image
{
    public long Id { get; set; } // Primary key
    public string Description { get; set; }
}