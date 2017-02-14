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

        public List<ParticipanteVM> partDBtoVM()
        {

            List<Participante> partList = participanteManager.SelectAll();
            List<ParticipanteVM> partVMList = new List<ParticipanteVM>();

            foreach (Participante part in partList)
            {

                ParticipanteVM partVM = new ParticipanteVM();
                partVM.Id = part.Id;
                partVM.Nome = part.Nome;

                partVMList.Add(partVM);

            }

            return partVMList;

        }

        // GET: Churrasco
        public ActionResult Index()
        {
            List<ChurrascoVM> churrasVMList = new List<ChurrascoVM>();
            List<Churrasco> churrasList = churrascoManager.SelectAll();
            foreach (var item in churrasList)
            {

                ChurrascoVM churrasVM = new ChurrascoVM();
                churrasVM.Id = item.Id;
                churrasVM.Data = item.Data;
                churrasVM.Razao = item.Razao;
                churrasVM.Obs = item.Obs;

                churrasVMList.Add(churrasVM);

            }

            return View(churrasVMList);

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

        public ActionResult AddPart(int id)
        {

            AddPartVM addPartVM = new AddPartVM();

            addPartVM.IdChurrasco = id;
            addPartVM.partVMList = this.partDBtoVM();

            return View(addPartVM);

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

        public ActionResult PartDB()
        {

            return View();

        }

        [HttpPost]
        public ActionResult PartDB(ParticipanteVM participante)
        {

            Participante dbParticipante = new Participante();

            if (ModelState.IsValid)
            {

                dbParticipante.Nome = participante.Nome;

                participanteManager.Insert(dbParticipante);

                return RedirectToAction("Index", "Churrasco");

            } else
            {

                return View(participante);

            }

        }

        [HttpPost]
        public ActionResult AddPart(AddPartVM addPartVM)
        {

            if (ModelState.IsValid)
            {

                ChurrascoParticipante dbCP = new ChurrascoParticipante();

                dbCP.IdChurrasco = addPartVM.IdChurrasco;
                dbCP.IdParticipante = addPartVM.IdParticipante;
                dbCP.CheckBebida = (!string.IsNullOrEmpty(addPartVM.strCheckBebida));
                dbCP.CheckPago = (!string.IsNullOrEmpty(addPartVM.strCheckPago));
                dbCP.Contribuicao = addPartVM.Contribuicao;
                dbCP.Obs = addPartVM.Obs;
                cpManager.Insert(dbCP);

                return RedirectToAction("Details", "Churrasco", new { id = dbCP.IdChurrasco });
                

            } else
            {

                addPartVM.partVMList = this.partDBtoVM();
                return View(addPartVM);

            }

        }

        [HttpPost]
        public ActionResult Edit(ChurrascoVM churras)
        {
            Churrasco dbChurrasco = new Churrasco();
            if (ModelState.IsValid)
            {

                //Método para salvar
                if (churras.Id.HasValue)
                {

                    dbChurrasco = churrascoManager.Get(k => k.Id == churras.Id);
                    dbChurrasco.Data = churras.Data;
                    dbChurrasco.Razao = churras.Razao;
                    dbChurrasco.Obs = churras.Obs;
                    churrascoManager.Update(dbChurrasco);

                } else
                {

                    dbChurrasco = new Churrasco();
                    dbChurrasco.Data = churras.Data;
                    dbChurrasco.Razao = churras.Razao;
                    dbChurrasco.Obs = churras.Obs;
                    churrascoManager.Insert(dbChurrasco);

                }
                return RedirectToAction("Details", new { id = dbChurrasco.Id });

            } else
            {

                return View(churras);

            }

        }
    }
}