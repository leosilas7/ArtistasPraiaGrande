using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using ArtistasPraiaGrande.Data;
using ArtistasPraiaGrande.Models;

namespace ArtistasPraiaGrande.Controllers
{

    public class ArtistasController : Controller
    {
        
        private readonly ArtistasPraiaGrandeDbContext _context;

        public ArtistasController(ArtistasPraiaGrandeDbContext context)
        {
            _context = context;
            
        }

        // GET: Artistas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artistas.Where(artistas => artistas.Ativo == 1 && artistas.Deferido == 1).ToListAsync());

            //return View(await _context.Artistas.ToListAsync());
        }
		
		        // GET: Indeferidos
        public async Task<IActionResult> Indeferidos()
        {
            return View(await _context.Artistas.Where(artistas => artistas.Ativo == 1 && artistas.Deferido == 0).ToListAsync());

            //return View(await _context.Artistas.ToListAsync());
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .FirstOrDefaultAsync(m => m.IdArtista == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArtista,NomeCompleto,NomeSocial,NomeArtistico,Ativo,Created,Modified,Deferido")] Artista artista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artista);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Cadastro Criado Com Sucesso. Aguardar Aprovação Do Cadastro...";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["AlertMessage"] = "Deu erro...";
                
            }
            
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();  
            }

            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null)
            {
                return NotFound();
            }
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdArtista,NomeCompleto,NomeSocial,NomeArtistico,Ativo,Created,Modified,Deferido")] Artista artista)
        
        {
            if (id != artista.IdArtista)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistaExists(artista.IdArtista))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["AlertMessage"] = "Cadastro Alterado Com Sucesso. ";
                return RedirectToAction(nameof(Index));
            }
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .FirstOrDefaultAsync(m => m.IdArtista == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _context.Artistas.FindAsync(id);
            //_context.Artistas.Remove(artista); 
            //mudando para ativo -> 0
            _context.Artistas.Update(artista);
            artista.Ativo = 0;
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Cadastro Deletado Com Sucesso. ";
            return RedirectToAction(nameof(Index));

        }
		
		// GET: Artistas/Deferir/5
        public async Task<IActionResult> Deferir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .FirstOrDefaultAsync(m => m.IdArtista == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // POST: Artistas/ConfirmarDeferimento/5
        [HttpPost, ActionName("Deferir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarDeferimento(int id)
        {
            var artista = await _context.Artistas.FindAsync(id);
            //_context.Artistas.Remove(artista); 
            //mudando para ativo -> 0
            _context.Artistas.Update(artista);
            artista.Deferido = 1;
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Cadastro Deferido Com Sucesso. ";
            return RedirectToAction(nameof(Index));

        }

        private bool ArtistaExists(int id)
        {
            return _context.Artistas.Any(e => e.IdArtista == id);
        }
    }
}
