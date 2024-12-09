//using SportsGoalApp.Areas.Identity.Data;
//using SportsGoalApp.Data;
//using SportsGoalApp.Models;

//namespace SportsGoalApp.Services
//{
//    public class DataTransferService
//    {
//        private readonly SportsGoalAppContext _sourceContext;
//        private readonly ForTestingDbContext _destinationContext;

//        public DataTransferService(SportsGoalAppContext sourceContext, ForTestingDbContext destinationContext)
//        {
//            _sourceContext = sourceContext;
//            _destinationContext = destinationContext;
//        }

//        public void TransferData()
//        {
//            var goalsToTransfer = _sourceContext.Goals.ToList();
//            var practicesToTransfer = _sourceContext.Practices.ToList();

//            foreach (var goal in goalsToTransfer)
//            {
//                var newGoal = new Goal
//                {
//                    // Map properties from the source goal
//                   Id = goal.Id,
//                   UserId = goal.UserId,
//                   Title = goal.Title,
//                   StartDate = goal.StartDate,
//                   EndDate = goal.EndDate,
//                   Description = goal.Description,
//                   Category = goal.Category,
//                   GoalNumber = goal.GoalNumber

//                };

//                _destinationContext.Goals.Add(newGoal);
//            }

//            foreach (var practice in practicesToTransfer)
//            {
//                var newPractice = new PracticeLog
//                {
//                    // Map properties from the source practice log
//                    Id= practice.Id,
//                    GoalId = practice.Id,
//                    UserId= practice.UserId,
//                    DateTime = DateTime.Now,
//                    Duration = practice.Duration,
//                    DurationUnit = practice.DurationUnit,
//                    Activity = practice.Activity,
//                    Notes = practice.Notes,
//                    TotalNumber = practice.TotalNumber,
//                    SuccessfulNumber = practice.SuccessfulNumber,
//                    Percentage = practice.Percentage
//                };

//                _destinationContext.Practices.Add(newPractice);
//            }

//            _destinationContext.SaveChanges();
//        }
//    }

//}
