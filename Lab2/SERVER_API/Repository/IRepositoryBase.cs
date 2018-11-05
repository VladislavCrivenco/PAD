using PAD.Models;
using System.Collections.Generic;

namespace PAD.Repository
{
    public interface IRepositoryBase
    {
        List<Authors> GetAuthors();
        List<Posts> GetPosts();
    }
}