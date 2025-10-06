using Microsoft.AspNetCore.Mvc;
using RegistrarPonto.Models;

namespace RegistrarPonto.Services
{
    public class MarkingsService
    {
        private readonly AppDbContext _appdbcontext;

        public MarkingsService(AppDbContext appcontext)
        {
            _appdbcontext = appcontext;
        }
    }
}
