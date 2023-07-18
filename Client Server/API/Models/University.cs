using System.ComponentModel;

namespace API.Models;
public class University
{
    public Guid Guid { get; set; } //uniq
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; } 
    public DateTime ModifiedDate { get; set;}
}

