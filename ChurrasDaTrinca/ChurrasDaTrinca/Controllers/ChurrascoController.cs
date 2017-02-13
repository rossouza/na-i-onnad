using ChurrasDaTrinca.DB.Business;
using ChurrasDaTrinca.DB.Database;
using ChurrasDaTrinca.DB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurrasDaTrinca.Controllers
{
    public class ChurrascoController : Controller
    {
        private ChurrascoManager churrascoManager;
        private ChurrascoParticipanteManager cpManager;
        private ParticipanteManager participanteManager;

        public ChurrascoController()
        {

            churrascoManager = new ChurrascoManager();
            cpManager = new ChurrascoParticipanteManager();
            participanteManager = new ParticipanteManager();

        }

        // GET: Churrasco
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {

            Churrasco dbChurrasco = churrascoManager.Get(k => k.Id == id);
            List<ChurrascoParticipante> cpList = cpManager.GetList(k => k.IdChurrasco == id);
            List<ChurrascoParticipanteVM> cpVMList = new List<ChurrascoParticipanteVM>();
            ChurrascoVM churrasVM = new ChurrascoVM();
            List<ParticipanteVM> partVMList = new List<ParticipanteVM>();

            churrasVM.Id = dbChurrasco.Id;
            churrasVM.Data = dbChurrasco.Data;
            churrasVM.Razao = dbChurrasco.Razao;
            churrasVM.Obs = dbChurrasco.Obs;

            foreach (ChurrascoParticipante cParticipante in cpList)
            {

                ChurrascoParticipanteVM cpVM = new ChurrascoParticipanteVM();
                ParticipanteVM partVM = new ParticipanteVM();
                Participante participante = participanteManager.Get(k => k.Id == cParticipante.IdParticipante);
                partVM.Id = participante.Id;
                partVM.Nome = participante.Nome;
                cpVM.Id = cParticipante.Id;
                cpVM.IdChurrasco = cParticipante.IdChurrasco;
                cpVM.IdParticipante = cParticipante.IdParticipante;
                cpVM.Contribuicao = cParticipante.Contribuicao;
                cpVM.CheckBebida = cParticipante.CheckBebida;
                cpVM.CheckPago = cParticipante.CheckPago;
                cpVM.Obs = cParticipante.Obs;
                cpVMList.Add(cpVM);
                partVMList.Add(partVM);

            }

            DetailsVM dVM = new DetailsVM();

            dVM.churrasco = churrasVM;
            dVM.cpVMList = cpVMList;
            dVM.partVMList = partVMList;

            return View(dVM);

        }

        [HttpPost]
        public ActionResult AddPart(DetailsVM dVM)
        {


            if (ModelState.IsValid)
            {

                

                return RedirectToAction("Details", "Churrasco", new { id = dVM.churrasco.Id });

            }
            else
            {

                return View(dVM);

            }


        }

        public ActionResult AddPart(int id)
        {

            Churrasco dbChurrasco = churrascoManager.Get(k => k.Id == id);
            List<ChurrascoParticipante> cpList = cpManager.GetList(k => k.IdChurrasco == id);
            List<ChurrascoParticipanteVM> cpVMList = new List<ChurrascoParticipanteVM>();
            ChurrascoVM churrasVM = new ChurrascoVM();
            List<ParticipanteVM> partVMList = new List<ParticipanteVM>();

            churrasVM.Id = dbChurrasco.Id;
            churrasVM.Data = dbChurrasco.Data;
            churrasVM.Razao = dbChurrasco.Razao;
            churrasVM.Obs = dbChurrasco.Obs;

            foreach (ChurrascoParticipante cParticipante in cpList)
            {

                ChurrascoParticipanteVM cpVM = new ChurrascoParticipanteVM();
                ParticipanteVM partVM = new ParticipanteVM();
                Participante participante = participanteManager.Get(k => k.Id == cParticipante.IdParticipante);
                partVM.Id = participante.Id;
                partVM.Nome = participante.Nome;
                cpVM.Id = cParticipante.Id;
                cpVM.IdChurrasco = cParticipante.IdChurrasco;
                cpVM.IdParticipante = cParticipante.IdParticipante;
                cpVM.Contribuicao = cParticipante.Contribuicao;
                cpVM.CheckBebida = cParticipante.CheckBebida;
                cpVM.CheckPago = cParticipante.CheckPago;
                cpVM.Obs = cParticipante.Obs;
                cpVMList.Add(cpVM);
                partVMList.Add(partVM);

            }

            DetailsVM dVM = new DetailsVM();

            dVM.churrasco = churrasVM;
            dVM.cpVMList = cpVMList;
            dVM.partVMList = partVMList;

            return View(dVM);

        }

        public void CheckPago(int idCP)
        {

            ChurrascoParticipante dbCP = cpManager.Get(k => k.Id == idCP);
            dbCP.CheckPago = true;
            cpManager.Update(dbCP);

        }

        public ActionResult Edit(int? id)
        {
            Churrasco dbChurrasco = churrascoManager.Get(k => k.Id == id);
            ChurrascoVM churrasVM = new ChurrascoVM();

            if (dbChurrasco != null)
            {

                churrasVM.Id = dbChurrasco.Id;
                churrasVM.Data = dbChurrasco.Data;
                churrasVM.Razao = dbChurrasco.Razao;
                churrasVM.Obs = dbChurrasco.Obs;

            } else
            {

                churrasVM.Data = DateTime.Now;

            }


            return View(churrasVM);
        }

        [HttpPost]
        public ActionResult Edit(ChurrascoVM churras)
        {

            if (ModelState.IsValid)
            {

                //Método para salvar
                if (churras.Id.HasValue)
                {

                    Churrasco dbChurrasco = churrascoManager.Get(k => k.Id == churras.Id);
                    dbChurrasco.Data = churras.Data;
                    dbChurrasco.Razao = churras.Razao;
                    dbChurrasco.Obs = churras.Obs;
                    churrascoManager.Update(dbChurrasco);

                } else
                {

                    Churrasco dbChurrasco = new Churrasco();
                    dbChurrasco.Data = churras.Data;
                    dbChurrasco.Razao = churras.Razao;
                    dbChurrasco.Obs = churras.Obs;
                    churrascoManager.Insert(dbChurrasco);

                }
                return RedirectToAction("Details", "Churrasco", new { id = churras.Id });

            } else
            {

                return View(churras);

            }

        }
    }
}