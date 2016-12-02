using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FinancialThing.Controllers
{
    public class DataController : Controller
    {
        private IRepository<Company, Guid> _dataRepo;
        private IRepository<ExpandedCompany, Guid> expCompRepo;

        public DataController(IRepository<Company, Guid> repo, IRepository<ExpandedCompany, Guid> expandedCompRepo)
        {
            _dataRepo = repo;
            expCompRepo = expandedCompRepo;
        }
        //
        // GET: /Data/
        public async Task<ActionResult> Index()
        {
            var states = Session["states"] as Dictionary<string, bool> ?? new Dictionary<string, bool>();

            //var data = await _dataRepo.GetQuery();

            var data = await expCompRepo.GetQuery();

            var dataItemViewModels = new List<DataItemViewModel>();

            var companies = new List<DataCompanyViewModel>();

            var dataViewModel = new DataViewModel { DataCompanyViewModels = companies };

            var revenueList = new List<string>
            {
                "TR",
                "CORT",
                "SGAAET",
                "OOET",
                "UEI",
                "DAM",
                "OI",
                "ON",
                "NIBT",
                "PFIT",
                "NIAT"

            }; // TODO move to config

            var minorityList = new List<string>
            {
                "MI2",
                "NIBEI",
                "TEI",
                "NI2",
                "IATCEEI",
                "IATCIEI"

            };

            var EPS = new List<string>
            {
                "BPWAS",
                "BPEEEI",
                "BPEIEI",
                "DA",
                "DWAS",
                "DEEEI",
                "DEIEI"
            };

            var CSD = new List<string>
            {
                "DCSPI",
                "GDCS"
            };

            var PFI = new List<string>
            {
                "PFNI",
                "IES"
            };

            var SI = new List<string>
            {
                "DS2",
                "TSI1"
            };

            var NI = new List<string>
            {
                "NINIBT",
                "EOSIOIT",
                "ITEIOSI",
                "NINIAT",
                "NIATC",
                "BNEPS",
                "DNEPS"
            };

            var ASS = new List<string>
            {
                "CASTI",
                "TRN",
                "TI",
                "PE",
                "OCAT",
                "PPEN",
                "GN",
                "IN",
                "LTI",
                "NRLT",
                "OLTA"
            };

            var totalCurAss = new List<string>
            {
                "CASTI",
                "TRN",
                "TI",
                "PE",
                "OCAT"
            };

            var totalNonCurAss = new List<string>
            {
                "PPEN",
                "GN",
                "IN",
                "LTI",
                "NRLT",
                "OLTA"
            };

            foreach (var company in data)
            {
                var dataCompanyViewModel = new DataCompanyViewModel();

                companies.Add(dataCompanyViewModel);

                dataCompanyViewModel.StatementViewModels = new List<StatementViewModel>();

                var incomeStatement = new StatementViewModel();
                incomeStatement.Name = "Income Statement";
                var balanceSheetViewMode = new StatementViewModel();
                balanceSheetViewMode.Name = "Balance Sheet";
                var cashFlowViewModel = new StatementViewModel();
                cashFlowViewModel.Name = "Cash Flow";

                var years = Enumerable.Range(company.MinYear, company.MaxYear - company.MinYear + 1).ToList();

                incomeStatement.Years = years.OrderByDescending(x => x).ToList();
                balanceSheetViewMode.Years = incomeStatement.Years;
                cashFlowViewModel.Years = incomeStatement.Years;
                var revenueDataItemViewModel = new DataItemViewModel();
                revenueDataItemViewModel.Name = "Revenue";

                incomeStatement.DataViewModels.Add(revenueDataItemViewModel);

                var id = company.Id.ToString();

                var revenueData = company.Data.Where(d => revenueList.Contains(d.Dictionary.Code));

                var revenue = new ValueViewModel();
                revenue.Name = "Revenue";

                revenueDataItemViewModel.ValueViewModels.Add(revenue);

                var costOfGoods = new ValueViewModel();
                costOfGoods.Name = "Cost of Goods Sold (COGS)";

                revenueDataItemViewModel.ValueViewModels.Add(costOfGoods);

                var grossProfits = new ValueViewModel();
                grossProfits.Name = "Gross Profit";

                revenueDataItemViewModel.ValueViewModels.Add(grossProfits);

                var SGAs = new ValueViewModel();
                SGAs.Name = "Selling, General and Admin Costs (SG&A)";

                revenueDataItemViewModel.ValueViewModels.Add(SGAs);

                var otherOpExpenses = new ValueViewModel();
                otherOpExpenses.Name = "Other Operating Expenses";

                revenueDataItemViewModel.ValueViewModels.Add(otherOpExpenses);

                var UEIs = new ValueViewModel();
                UEIs.Name = "Unusual Expenses (Income)";

                revenueDataItemViewModel.ValueViewModels.Add(UEIs);

                var discrepancies = new ValueViewModel();
                discrepancies.Name = "Discrepancies";

                revenueDataItemViewModel.ValueViewModels.Add(discrepancies);

                var depAndAm = new ValueViewModel();
                depAndAm.Name = "Depreciation and Amortization (D&A)";

                revenueDataItemViewModel.ValueViewModels.Add(depAndAm);

                var EBIT = new ValueViewModel();
                EBIT.Name = "Operating Income (EBIT)";

                revenueDataItemViewModel.ValueViewModels.Add(EBIT);

                var ON = new ValueViewModel();
                ON.Name = "Other Income, net";

                revenueDataItemViewModel.ValueViewModels.Add(ON);

                var EBITDA = new ValueViewModel();
                EBITDA.Name = "EBITDA";

                revenueDataItemViewModel.ValueViewModels.Add(EBITDA);

                var PBIT = new ValueViewModel();
                PBIT.Name = "PBIT";

                revenueDataItemViewModel.ValueViewModels.Add(PBIT);

                var NFE = new ValueViewModel();
                NFE.Name = "Net Financial Expense (NFE)";

                revenueDataItemViewModel.ValueViewModels.Add(NFE);

                var PBT = new ValueViewModel();
                PBT.Name = "Profit Before Tax (PBT)";

                revenueDataItemViewModel.ValueViewModels.Add(PBT);

                var PFIT = new ValueViewModel();
                PFIT.Name = "Income Tax Expense";

                revenueDataItemViewModel.ValueViewModels.Add(PFIT);

                var NIAT = new ValueViewModel();
                NIAT.Name = "Net Income (PAT)";

                revenueDataItemViewModel.ValueViewModels.Add(NIAT);


                var totalRevenues = revenueData.First(d => d.Dictionary.Code == "TR").Values.OrderByDescending(x => x.Year).ToArray();
                var costsOfGoodSold = revenueData.First(d => d.Dictionary.Code == "CORT").Values.OrderByDescending(x => x.Year).ToArray();
                var SGAAETs =
                    revenueData.First(d => d.Dictionary.Code == "SGAAET")
                        .Values.OrderByDescending(x => x.Year)
                        .ToArray();

                var otherOperatingExpenses =
                    revenueData.First(d => d.Dictionary.Code == "OOET").Values.OrderByDescending(x => x.Year).ToArray();

                var unusualExpenses =
                    revenueData.First(d => d.Dictionary.Code == "UEI").Values.OrderByDescending(x => x.Year).ToArray();

                var DAs =
                    revenueData.First(d => d.Dictionary.Code == "DAM").Values.OrderByDescending(x => x.Year).ToArray();

                var otherIncomeNets =
                    revenueData.First(d => d.Dictionary.Code == "ON").Values.OrderByDescending(x => x.Year).ToArray();

                var PBTs =
                    revenueData.First(d => d.Dictionary.Code == "NIBT").Values.OrderByDescending(x => x.Year).ToArray();

                var EBITs =
                    revenueData.First(d => d.Dictionary.Code == "OI").Values.OrderByDescending(x => x.Year).ToArray();

                var PFITs =
                    revenueData.First(d => d.Dictionary.Code == "PFIT").Values.OrderByDescending(x => x.Year).ToArray();

                var NIATs =
                    revenueData.First(d => d.Dictionary.Code == "NIAT").Values.OrderByDescending(x => x.Year).ToArray();
                

                var yearsRange = company.MaxYear - company.MinYear;

                for (int i = 0; i <= yearsRange; i++)
                {
                    //IncomeStatement
                    revenue.Values.Add(totalRevenues[i].DataValue);
                    costOfGoods.Values.Add(costsOfGoodSold[i].DataValue);
                    var grossProfit = totalRevenues[i].DataValue - costsOfGoodSold[i].DataValue;
                    grossProfits.Values.Add(grossProfit);
                    SGAs.Values.Add(SGAAETs[i].DataValue);
                    otherOpExpenses.Values.Add(otherOperatingExpenses[i].DataValue);
                    UEIs.Values.Add(unusualExpenses[i].DataValue);
                    //discrepancies.Values.Add(); TODO add
                    var ebitda = grossProfit - (SGAAETs[i].DataValue + otherOperatingExpenses[i].DataValue + unusualExpenses[i].DataValue);
                    EBITDA.Values.Add(ebitda);
                    depAndAm.Values.Add(DAs[i].DataValue);
                    EBIT.Values.Add(EBITs[i].DataValue);
                    ON.Values.Add(otherIncomeNets[i].DataValue);
                    var pbit = EBITs[i].DataValue + otherIncomeNets[i].DataValue;
                    PBIT.Values.Add(pbit);
                    var nfe = PBTs[i].DataValue - pbit;
                    NFE.Values.Add(nfe);
                    PBT.Values.Add(PBTs[i].DataValue);
                    PFIT.Values.Add(PFITs[i].DataValue);
                    NIAT.Values.Add(NIATs[i].DataValue);
                    //BalanceSheet -------


                }

                var minorityDataItemViewModel = new DataItemViewModel();
                minorityDataItemViewModel.Name = "Minority Interest and Extra Items";

                foreach (var minority in minorityList)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == minority);
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    minorityDataItemViewModel.ValueViewModels.Add(valueVM);
                }

                incomeStatement.DataViewModels.Add(minorityDataItemViewModel);

                var EPSItemViewModel = new DataItemViewModel();
                EPSItemViewModel.Name = "EPS Reconciliation";

                foreach (var eps in EPS)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == eps);
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    EPSItemViewModel.ValueViewModels.Add(valueVM);
                }

                incomeStatement.DataViewModels.Add(EPSItemViewModel);

                var CSDDataItemViewModel = new DataItemViewModel();
                CSDDataItemViewModel.Name = "Common Stock Dividends";

                foreach (var csd in CSD)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == csd);
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    CSDDataItemViewModel.ValueViewModels.Add(valueVM);
                }

                incomeStatement.DataViewModels.Add(CSDDataItemViewModel);


                var PFIDataItemViewModel = new DataItemViewModel();
                PFIDataItemViewModel.Name = "Pro Forma Income";

                foreach (var pfi in PFI)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == pfi);
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    PFIDataItemViewModel.ValueViewModels.Add(valueVM);
                }

                incomeStatement.DataViewModels.Add(PFIDataItemViewModel);

                var SIDataItemViewModel = new DataItemViewModel();
                SIDataItemViewModel.Name = "Supplemental Income";

                foreach (var si in SI)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == si && d.Dictionary.ParentCode == "SupplInc");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    SIDataItemViewModel.ValueViewModels.Add(valueVM);
                }

                incomeStatement.DataViewModels.Add(SIDataItemViewModel);

                var NIDataItemViewModel = new DataItemViewModel();

                NIDataItemViewModel.Name = "Normalized Income";

                foreach (var ni in NI)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni);
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    NIDataItemViewModel.ValueViewModels.Add(valueVM);
                }

                incomeStatement.DataViewModels.Add(NIDataItemViewModel);

                //Balance sheet

                var CEDataItemViewModel = new DataItemViewModel();

                CEDataItemViewModel.Name = "Assets";

                foreach (var ni in ASS)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni);
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    CEDataItemViewModel.ValueViewModels.Add(valueVM);
                }

                var totalCurAssVM = new ValueViewModel();
                totalCurAssVM.Name = "Total Current Assets";
                var totNonCurAssVM = new ValueViewModel();
                totNonCurAssVM.Name = "Total Non-Current Assets";
                var totalAssVM = new ValueViewModel();
                totalAssVM.Name = "TOTAL ASSETS";

                CEDataItemViewModel.ValueViewModels.Add(totalCurAssVM);
                CEDataItemViewModel.ValueViewModels.Add(totNonCurAssVM);
                CEDataItemViewModel.ValueViewModels.Add(totalAssVM);

                for (int i = 0; i <= yearsRange; i++)
                {
                    var sum = 0m;
                    var currAss = company.Data.Where(x => totalCurAss.Contains(x.Dictionary.Code));
                    foreach (var val in currAss)
                    {
                        sum += val.Values.OrderByDescending(x => x.Year).ToArray()[i].DataValue;
                    }

                    totalCurAssVM.Values.Add(sum);

                    var sum1 = 0m;
                    var curNonAss = company.Data.Where(x => totalNonCurAss.Contains(x.Dictionary.Code));
                    foreach (var val in curNonAss)
                    {
                        sum1 += val.Values.OrderByDescending(x => x.Year).ToArray()[i].DataValue;
                    }

                    totNonCurAssVM.Values.Add(sum1);

                    totalAssVM.Values.Add(sum+sum1);
                }

                balanceSheetViewMode.DataViewModels.Add(CEDataItemViewModel);

                var liabilitiesCodes = new List<string>
                {
                    "AP",
                    "AE",
                    "NP",
                    "CPLTBCL",
                    "OCLT",
                    "TLTD",
                    "TD",
                    "DIT",
                    "MI",
                    "OLT"
                };

                var liabilitiesItemViewModel = new DataItemViewModel();
                liabilitiesItemViewModel.Name = "LIABILITIES";

                foreach (var ni in liabilitiesCodes)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni && d.Dictionary.ParentCode == "Liabilities");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    liabilitiesItemViewModel.ValueViewModels.Add(valueVM);
                }

                var totalCurLiabVM = new ValueViewModel();
                totalCurLiabVM.Name = "Total Current Liabilities";
                liabilitiesItemViewModel.ValueViewModels.Add(totalCurLiabVM);

                var totalCurLiabSums = new List<string>
                {
                    "AP",
                    "AE",
                    "NP",
                    "CPLTBCL",
                    "OCLT"
                };

                var totalCurLiabVals = company.Data.Where(x => totalCurLiabSums.Contains(x.Dictionary.Code));

                var totalNonCurLiabVM = new ValueViewModel();
                totalNonCurLiabVM.Name = "Total Non-Current Liabilities";
                liabilitiesItemViewModel.ValueViewModels.Add(totalNonCurLiabVM);

                var totalLiabVM = new ValueViewModel();
                totalLiabVM.Name = "Total Liabilities";
                liabilitiesItemViewModel.ValueViewModels.Add(totalLiabVM);

                var totalNonCurLiabSums = new List<string>
                {
                    "TLTD",
                    "TD",
                    "DIT",
                    "MI",
                    "OLT"
                };

                var totalNonCurLiabVals = company.Data.Where(x => totalNonCurLiabSums.Contains(x.Dictionary.Code));

                for (int i = 0; i <= yearsRange; i++)
                {
                    var sum = 0m;
                    foreach (var totalCurLiabVal in totalNonCurLiabVals)
                    {
                        sum += totalCurLiabVal.Values.OrderByDescending(x => x.Year).ToArray()[i].DataValue;
                    }

                    totalNonCurLiabVM.Values.Add(sum);

                    var totalCurLiabSum = 0m;
                    foreach (var totalCurLiabVal in totalCurLiabVals)
                    {
                        totalCurLiabSum += totalCurLiabVal.Values.OrderByDescending(x => x.Year).ToArray()[i].DataValue;
                    }

                    totalCurLiabVM.Values.Add(totalCurLiabSum);

                    totalLiabVM.Values.Add(totalCurLiabSum+sum);
                }

                balanceSheetViewMode.DataViewModels.Add(liabilitiesItemViewModel);

                var SE = new List<string>
                {
                    "CS",
                    "APIC",
                    "READ",
                    "TSC",
                    "UGL",
                    "OET"
                };

                var SEItemViewModel = new DataItemViewModel();
                SEItemViewModel.Name = "SHAREHOLDERS' EQUITY";

                var totalEQ = 0m;

                foreach (var ni in SE)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni && d.Dictionary.ParentCode == "ShareholdersEquity");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    SEItemViewModel.ValueViewModels.Add(valueVM);
                }
                balanceSheetViewMode.DataViewModels.Add(SEItemViewModel);

                var TLSE = new List<string>
                {
                    "TCSO",
                    "TSCPI"
                };

                var TLSEItemViewModel = new DataItemViewModel();
                TLSEItemViewModel.Name = "TOTAL LIABILITIES & SHAREHOLDERS' EQUITY";

                foreach (var ni in TLSE)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni && d.Dictionary.ParentCode == "ShareholdersEquity");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    TLSEItemViewModel.ValueViewModels.Add(valueVM);
                }
                balanceSheetViewMode.DataViewModels.Add(TLSEItemViewModel);

                var CFFO = new List<string>
                {
                    "NI",
                    "DD",
                    "NCI",
                    "CTPS",
                    "CIPS",
                    "CIWC",
                    "TCFO"
                };

                var CFFOItemViewModel = new DataItemViewModel();
                CFFOItemViewModel.Name = "Cash Flows From Operations";

                foreach (var ni in CFFO)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni && d.Dictionary.ParentCode == "operations");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    CFFOItemViewModel.ValueViewModels.Add(valueVM);
                }
                cashFlowViewModel.DataViewModels.Add(CFFOItemViewModel);

                var CFFIA = new List<string>
                {
                    "CE",
                    "OIACFIT",
                    "TCFI"
                };

                var CFFIAViewModel = new DataItemViewModel();
                CFFIAViewModel.Name = "Cash Flows from Investing Activities";

                foreach (var ni in CFFIA)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni && d.Dictionary.ParentCode == "investing");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    CFFIAViewModel.ValueViewModels.Add(valueVM);
                }
                cashFlowViewModel.DataViewModels.Add(CFFIAViewModel);

                var CFFFA = new List<string>
                {
                    "FCFI",
                    "TCDP",
                    "IROSN",
                    "IRODN",
                    "TCFF"
                };

                var CFFFAViewModel = new DataItemViewModel();
                CFFFAViewModel.Name = "Cash Flows from Financing Activities";

                foreach (var ni in CFFFA)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni && d.Dictionary.ParentCode == "financing");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    CFFFAViewModel.ValueViewModels.Add(valueVM);
                }
                cashFlowViewModel.DataViewModels.Add(CFFFAViewModel);

                var NCIC = new List<string>
                {
                    "FEE",
                    "NCBBRFFU",
                    "NCIC",
                    "NCEBRFFU"
                };

                var NCICViewModel = new DataItemViewModel();
                NCICViewModel.Name = "Net Change in Cash";
                foreach (var ni in NCIC)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni && d.Dictionary.ParentCode == "NetChangeInCash");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    NCICViewModel.ValueViewModels.Add(valueVM);
                }
                cashFlowViewModel.DataViewModels.Add(NCICViewModel);

                var SINC = new List<string>
                {
                    "DS",
                    "CIPS2",
                    "CTPS"
                };

                var SINCViewModel = new DataItemViewModel();
                SINCViewModel.Name = "Supplemental Income";
                foreach (var ni in SINC)
                {
                    var value = company.Data.First(d => d.Dictionary.Code == ni && d.Dictionary.ParentCode == "SupplementalIncome");
                    var valueVM = new ValueViewModel();

                    valueVM.Values = value.Values.OrderByDescending(v => v.Year).Select(x => x.DataValue).ToList();
                    valueVM.Name = value.Dictionary.DisplayName;

                    SINCViewModel.ValueViewModels.Add(valueVM);
                }
                cashFlowViewModel.DataViewModels.Add(SINCViewModel);

                dataCompanyViewModel.StatementViewModels.Add(incomeStatement);
                dataCompanyViewModel.StatementViewModels.Add(balanceSheetViewMode);
                dataCompanyViewModel.StatementViewModels.Add(cashFlowViewModel);
                /* dataViewModels.Add(new DataViewModel()
                 {
                     CompanyViewModel = new CompanyViewModel()
                     {
                         DisplayName = datum.FullName,
                         Id = id,
                         Toggle = new Toggle()
                         {
                             Id = id,
                             State = states.ContainsKey(id) ? states[id] : false,
                         },
                         Active = "hide"
                     },
                     //Financials = datum.Financials,
                 });
                 var active = dataViewModels.FirstOrDefault(c=>c.CompanyViewModel.Toggle.State == true);
                 if (active != null)
                 {
                     active.CompanyViewModel.Active = "show";
                 }*/
            }
            //return View(dataViewModels);
            return View("Index2", dataViewModel);
        }

        public async Task<ActionResult> GenerateAll()
        {
            await _dataRepo.Add(null);
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> Generate(string id)
        {
            await _dataRepo.SaveOrUpdate(new Company() { Id = new Guid(id) });
            return null;
        }
    }
}