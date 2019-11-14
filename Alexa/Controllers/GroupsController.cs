using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Alexa.Service.Interfaces;
using Alexa.Service;
using Alexa.Service.Dtos;

namespace Alexa.Controllers
{
    public class GroupsController : Controller
    {
        private ISupplierService SupplierService = new SupplierService();
        private IGroupService GroupService = new GroupService();

        // GET: Groups
        public ActionResult Index()
        {
            return View(GroupService.GetAll());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupDto group = GroupService.GetById(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            GroupDto group = GroupService.Create();
            group.AllSuppliers = SupplierService.getAllSuppliers();

            return View(group);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,SelectedItems")] GroupDto group)
        {
            if (ModelState.IsValid)
            {
                if (group.SelectedItems != null
                    && group.SelectedItems.Length > 0)
                {
                    group.Suppliers = new List<SupplierDto>();

                    /* Split SelectedSuppliers by ';' */
                    string[] supplierStrs = group.SelectedItems.Split(';');

                    /* Iterate through groupStrs, add selected groups into supplierDb and remeber tested groups */
                    foreach (string supplierStr in supplierStrs)
                    {
                        try
                        {
                            int id = Int32.Parse(supplierStr.Replace("Supplier_", ""));
                            group.Suppliers.Add(SupplierService.GetById(id));
                        }
                        catch (FormatException)
                        {
                            ;
                        }
                    }
                }

                GroupService.Add(group);

                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupDto group = GroupService.GetById(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            group.AllSuppliers = SupplierService.getAllSuppliers();

            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,SelectedItems")] GroupDto group)
        {
            if (ModelState.IsValid)
            {
                GroupDto groupDb = GroupService.GetById(group.ID);
                groupDb.Name = group.Name;

                var checkedSuppliers = new List<SupplierDto>();

                if (group.SelectedItems != null
                    && group.SelectedItems.Length > 0)
                {
                    group.Suppliers = new List<SupplierDto>();

                    /* Split SelectedSuppliers by ';' */
                    string[] supplierStrs = group.SelectedItems.Split(';');

                    /* Iterate through supplierStrs, add selected suppliers into groupDb and remeber tested suppliers */
                    foreach (string supplierStr in supplierStrs)
                    {
                        try
                        {
                            int id = Int32.Parse(supplierStr.Replace("Supplier_", ""));

                            SupplierDto supplier = SupplierService.GetById(id);
                            group.Suppliers.Add(supplier);

                            /* Mark supplier as tested */
                            checkedSuppliers.Add(supplier);

                            if (groupDb.Suppliers.Contains(supplier) == false)
                            {
                                groupDb.Suppliers.Add(supplier);
                            }
                        }
                        catch (FormatException)
                        {
                            ;
                        }
                    }
                }

                /* Select suppliers for removing */
                var suppliersToRemove = new List<SupplierDto>();
                foreach (SupplierDto supplier in groupDb.Suppliers)
                {
                    if (checkedSuppliers.Contains(supplier) == false)
                    {
                        suppliersToRemove.Add(supplier);
                    }
                }

                /* Remove selected suppliers */
                foreach (var supplier in suppliersToRemove)
                {
                    groupDb.Suppliers.Remove(supplier);
                }

                /* Save changes into DB */
                GroupService.Edit(groupDb);

                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupDto group = GroupService.GetById(id);

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupService.Delete(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (SupplierService is IDisposable)
            {
                ((IDisposable)SupplierService).Dispose();
            }

            if (GroupService is IDisposable)
            {
                ((IDisposable)GroupService).Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
