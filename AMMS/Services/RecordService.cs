using AMMS.Models;
using AMMS.Models.ViewModels;
using AMMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Services
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _repository;

        public RecordService(IRecordRepository repository)
        {
            _repository = repository;
        }

        public FlightViewModel GetFlightById(string id)
        {
            return MapToFlightViewModel(_repository.GetFlightById(id));
        }

        public IEnumerable<FlightViewModel> GetFlightsByAircraftId(string aircraftId)
        {
            var flights = _repository.GetFlightsByAircraftId(aircraftId);
            return flights.Select(MapToFlightViewModel).ToList();
        }

        public InspectionViewModel GetInspectionById(string id)
        {
            return MapToInspectionViewModel(_repository.GetInspectionById(id));
        }

        public IEnumerable<InspectionViewModel> GetInspectionsByAircraftId(string id)
        {
            var inspections = _repository.GetInspectionsByAircraftId(id);
            return inspections.Select(MapToInspectionViewModel).ToList();
        }

        public FaultViewModel GetFaultById(string id)
        {
            return MapToFaultViewModel(_repository.GetFaultById(id));
        }

        public IEnumerable<FaultViewModel> GetFaultsByAircraftId(string id)
        {
            var faults = _repository.GetFaultsByAircraftId(id);
            return faults.Select(MapToFaultViewModel).ToList();
        }

        public RelatedMaintenanceViewModel GetRelatedMaintenanceById(string id)
        {
            return MapToRelatedMaintenanceViewModel(_repository.GetRelatedMaintenanceById(id));
        }

        public IEnumerable<RelatedMaintenanceViewModel> GetRelatedMaintenanceByFaultId(string id)
        {
            var related = _repository.GetRelatedMaintenanceByFaultId(id);
            return related.Select(MapToRelatedMaintenanceViewModel).ToList();
        }

        public Aircraft GetAircraftById(string id)
        {
            return _repository.GetAircraftById(id);
        }

        public Unit GetUnitById(string id)
        {
            return _repository.GetUnitById(id);
        }

        public AircraftModel GetAircraftModelById(string id)
        {
            return _repository.GetAircraftModelById(id);
        }

        public void CreateFlight(FlightViewModel flight)
        {
            var aircraft = _repository.GetAircraftById(flight.AircraftId);
            aircraft.AcftHrs += flight.FlightHours;
            _repository.CreateFlight(MapToFlight(flight));
        }

        public void UpdateFlight(FlightViewModel flight)
        {
            var flt = _repository.GetFlightById(flight.Id);
            var aircraft = _repository.GetAircraftById(flight.AircraftId);
            if (Math.Abs(flight.FlightHours - flt.FlightHours) > 0.1)
            {
                aircraft.AcftHrs -= flt.FlightHours;
                aircraft.AcftHrs += flight.FlightHours;
            }
            CopyToFlight(flight, flt);

            _repository.UpdateFlight(flt);
        }

        public void DeleteFlight(string id)
        {
            var flight = _repository.GetFlightById(id);
            var aircraft = _repository.GetAircraftById(flight.AircraftId);
            aircraft.AcftHrs -= flight.FlightHours;
            _repository.DeleteFlight(id);
        }

        public void CreateInspection(InspectionViewModel inspection)
        {
            _repository.CreateInspection(MapToInspection(inspection));
        }

        public void UpdateInspection(InspectionViewModel inspection)
        {
            _repository.UpdateInspection(MapToInspection(inspection));
        }

        public void DeleteInspection(string id)
        {
            _repository.DeleteInspection(id);
        }

        public void CreateFault(FaultViewModel fault)
        {
            _repository.CreateFault(MapToFault(fault));
        }

        public void UpdateFault(FaultViewModel fault)
        {
            _repository.UpdateFault(MapToFault(fault));
        }

        public void DeleteFault(string id)
        {
            _repository.DeleteFault(id);
        }

        public void CreateRelatedMaintenance(RelatedMaintenanceViewModel related)
        {
            _repository.CreateRelatedMaintenance(MapToRelatedMaintenance(related));
        }

        public void UpdateRelatedMaintenance(RelatedMaintenanceViewModel related)
        {
            _repository.UpdateRelatedMaintenance(MapToRelatedMaintenance(related));
        }

        public void DeleteRelatedMaintenance(string id)
        {
            _repository.DeleteRelatedMaintenance(id);
        }

        public FlightViewModel MapToFlightViewModel(Flight record)
        {
            var aircraft = _repository.GetAircraftById(record.AircraftId);
            var acftModel = _repository.GetAircraftModelById(aircraft.AircraftModelId);
            var unit = _repository.GetUnitById(aircraft.UnitId);
            return new FlightViewModel
            {
                Id = record.Id,
                Date = record.Date,
                SerialNumber = aircraft.SerialNumber,
                Model = acftModel.Mds,
                Organization = unit.UnitName,
                Station = unit.Station,
                FlightNumber = record.FlightNumber,
                From = record.From,
                To = record.To,
                Start = record.Start,
                End = record.End,
                FlightHours = record.FlightHours,
                AircraftId = record.AircraftId
            };
        }

        public static Flight MapToFlight(FlightViewModel flight)
        {
            return new Flight
            {
                Id = flight.Id,
                Date = flight.Date,
                FlightNumber = flight.FlightNumber,
                From = flight.From,
                To = flight.To,
                Start = flight.Start,
                End = flight.End,
                FlightHours = flight.FlightHours,
                AircraftId = flight.AircraftId
            };
        }

        private static void CopyToFlight(FlightViewModel viewModel, Flight flight)
        {
            flight.Id = viewModel.Id;
            flight.Date = viewModel.Date;
            flight.FlightNumber = viewModel.FlightNumber;
            flight.From = viewModel.From;
            flight.To = viewModel.To;
            flight.Start = viewModel.Start;
            flight.End = viewModel.End;
            flight.FlightHours = viewModel.FlightHours;
            flight.AircraftId = viewModel.AircraftId;
        }

        private InspectionViewModel MapToInspectionViewModel(Inspection inspection)
        {
            var aircraft = _repository.GetAircraftById(inspection.AircraftId);
            var acftModel = _repository.GetAircraftModelById(aircraft.AircraftModelId);
            return new InspectionViewModel
            {
                Id = inspection.Id,
                Nomenclature = acftModel.Name,
                Model = acftModel.Mds,
                SerialNumber = aircraft.SerialNumber,
                InspectionNumber = inspection.InspectionNumber,
                ItemToBeInspected = inspection.ItemToBeInspected,
                Reference = inspection.Reference,
                Frequency = inspection.Frequency,
                NextDue = inspection.NextDue,
                CompletedAt = inspection.CompletedAt,
                AircraftId = inspection.AircraftId
            };
        }

        private static Inspection MapToInspection(InspectionViewModel inspection)
        {
            return new Inspection
            {
                Id = inspection.Id,
                InspectionNumber = inspection.InspectionNumber,
                ItemToBeInspected = inspection.ItemToBeInspected,
                Reference = inspection.Reference,
                Frequency = inspection.Frequency,
                NextDue = inspection.NextDue,
                CompletedAt = inspection.CompletedAt,
                AircraftId = inspection.AircraftId
            };
        }

        private static void CopyToInspection(InspectionViewModel viewModel, Inspection inspection)
        {
            inspection.Id = viewModel.Id;
            inspection.InspectionNumber = viewModel.InspectionNumber;
            inspection.ItemToBeInspected = viewModel.ItemToBeInspected;
            inspection.Reference = viewModel.Reference;
            inspection.Frequency = viewModel.Frequency;
            inspection.NextDue = viewModel.NextDue;
            inspection.CompletedAt = viewModel.CompletedAt;
            inspection.AircraftId = viewModel.AircraftId;
        }

        private FaultViewModel MapToFaultViewModel(Fault fault)
        {
            var aircraft = _repository.GetAircraftById(fault.AircraftId);
            var acftModel = _repository.GetAircraftModelById(aircraft.AircraftModelId);
            return new FaultViewModel
            {
                Id = fault.Id,
                AcftSerialNumber = aircraft.SerialNumber,
                AcftModel = acftModel.Mds,
                Status = fault.Status,
                SystemCode = fault.SystemCode,
                FaultDate = fault.FaultDate,
                FaultNumber = fault.FaultNumber,
                FaultTime = fault.FaultTime,
                DiscPID = fault.DiscPID,
                FaultText = fault.FaultText,
                DiscAcftHrs = fault.DiscAcftHrs,
                WhenDisc = fault.WhenDisc,
                HowRecog = fault.HowRecog,
                MalEff = fault.MalEff,
                Delay = fault.Delay,
                WUC = fault.WUC,
                CompDate = fault.CompDate,
                CompTime = fault.CompTime,
                CompAcftHrs = fault.CompAcftHrs,
                Rounds = fault.Rounds,
                ActionCode = fault.ActionCode,
                CompWUC = fault.CompWUC,
                Action = fault.Action,
                CompPID = fault.CompPID,
                CompCat = fault.CompCat,
                CompHrs = fault.CompHrs,
                AircraftId = fault.AircraftId
            };
        }

        private static Fault MapToFault(FaultViewModel fault)
        {
            return new Fault
            {
                Id = fault.Id,
                Status = fault.Status,
                SystemCode = fault.SystemCode,
                FaultDate = fault.FaultDate,
                FaultNumber = fault.FaultNumber,
                FaultTime = fault.FaultTime,
                DiscPID = fault.DiscPID,
                FaultText = fault.FaultText,
                DiscAcftHrs = fault.DiscAcftHrs,
                WhenDisc = fault.WhenDisc,
                HowRecog = fault.HowRecog,
                MalEff = fault.MalEff,
                Delay = fault.Delay,
                WUC = fault.WUC,
                CompDate = fault.CompDate,
                CompTime = fault.CompTime,
                CompAcftHrs = fault.CompAcftHrs,
                Rounds = fault.Rounds,
                ActionCode = fault.ActionCode,
                CompWUC = fault.CompWUC,
                Action = fault.Action,
                CompPID = fault.CompPID,
                CompCat = fault.CompCat,
                CompHrs = fault.CompHrs,
                AircraftId = fault.AircraftId
            };
        }

        private static void CopyToFault(FaultViewModel viewModel, Fault fault)
        {
            fault.Id = viewModel.Id;
            fault.Status = viewModel.Status;
            fault.SystemCode = viewModel.SystemCode;
            fault.FaultDate = viewModel.FaultDate;
            fault.FaultNumber = viewModel.FaultNumber;
            fault.FaultTime = viewModel.FaultTime;
            fault.DiscPID = viewModel.DiscPID;
            fault.FaultText = viewModel.FaultText;
            fault.DiscAcftHrs = viewModel.DiscAcftHrs;
            fault.WhenDisc = viewModel.WhenDisc;
            fault.HowRecog = viewModel.HowRecog;
            fault.MalEff = viewModel.MalEff;
            fault.Delay = viewModel.Delay;
            fault.WUC = viewModel.WUC;
            fault.CompDate = viewModel.CompDate;
            fault.CompTime = viewModel.CompTime;
            fault.CompAcftHrs = viewModel.CompAcftHrs;
            fault.Rounds = viewModel.Rounds;
            fault.ActionCode = viewModel.ActionCode;
            fault.CompWUC = viewModel.CompWUC;
            fault.Action = viewModel.Action;
            fault.CompPID = viewModel.CompPID;
            fault.CompCat = viewModel.CompCat;
            fault.CompHrs = viewModel.CompHrs;
            fault.AircraftId = viewModel.AircraftId;
        }

        private RelatedMaintenanceViewModel MapToRelatedMaintenanceViewModel(RelatedMaintenance fault)
        {
            var f = _repository.GetFaultById(fault.Id);
            var acft = _repository.GetAircraftById(f.AircraftId);

            return new RelatedMaintenanceViewModel
            {
                Id = fault.Id,
                FaultStatus = f.Status,
                SerialNumber = acft.SerialNumber,
                SystemCode = f.SystemCode,
                FaultDate = f.FaultDate,
                FaultNumber = f.FaultNumber,
                FaultText = f.FaultText,
                Status = fault.Status,
                RelatedMaintenanceAction = fault.RelatedMaintenanceAction,
                CorrectiveAction = fault.CorrectiveAction,
                PID = fault.PID,
                Category = fault.Category,
                MMH = fault.MMH,
                FaultId = fault.FaultId
            };
        }

        private RelatedMaintenance MapToRelatedMaintenance(RelatedMaintenanceViewModel fault)
        {
            return new RelatedMaintenance
            {
                Id = fault.Id,
                Status = fault.Status,
                RelatedMaintenanceAction = fault.RelatedMaintenanceAction,
                CorrectiveAction = fault.CorrectiveAction,
                PID = fault.PID,
                Category = fault.Category,
                MMH = fault.MMH,
                FaultId = fault.FaultId
            };
        }

        private void CopyToRelatedMaintenance(RelatedMaintenanceViewModel viewModel, RelatedMaintenance fault)
        {
            fault.Id = viewModel.Id;
            fault.Status = viewModel.Status;
            fault.RelatedMaintenanceAction = viewModel.RelatedMaintenanceAction;
            fault.CorrectiveAction = viewModel.CorrectiveAction;
            fault.PID = viewModel.PID;
            fault.Category = viewModel.Category;
            fault.MMH = viewModel.MMH;
            fault.FaultId = viewModel.FaultId;
        }
    }
}
