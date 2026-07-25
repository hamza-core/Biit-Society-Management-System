using project.Models;
using project.DataHandlers.ViewModel;
using System;
using System.Collections.Generic;
using project.DataHandlers;

namespace project.Utilities
{
    public static class NotificationService
    {
        public static void NotifyStudentJoinRequest(string studentAridNo, int societyId)
        {
            var student = StudentViewModel.GetByAridNo(studentAridNo);
            var society = SocietyViewModel.GetById(societyId.ToString());
            var president = SocietyMemberViewModel.GetPresident(societyId);

            if (president == null) return;

            NotificationViewModel.AddNotification(new Notification
            {
                Title = "New Society Join Request",
                Message = $"{student.Name} requested to join {society.Name}.",
                RecipientAridNo = president.AridNo,
                SenderId = student.AridNo,
                SocietyID = societyId,
                CreatedAt = DateTime.Now
            });
        }
        public static void NotifyMemberAddedToTeam(string studentAridNo, int societyId, string addedByAridNo, string teamName)
        {
            Notification notification = new Notification
            {
                Title = "Added to Team",
                Message = $"You have been added to the team \"{teamName}\" in your society.",
                RecipientAridNo = studentAridNo,
                SocietyID = societyId,
                SenderId = addedByAridNo,
                CreatedAt = DateTime.Now,
                IsRead = false,
                EventId = null
            };

            NotificationViewModel.AddNotification(notification);
        }


        public static void NotifyJoinApproved(string studentAridNo, string approverAridNo, int societyId)
        {
            var society = SocietyViewModel.GetById(societyId.ToString());

            NotificationViewModel.AddNotification(new Notification
            {
                Title = "Join Request Approved",
                Message = $"Your request to join {society.Name} has been approved.",
                RecipientAridNo = studentAridNo,
                SenderId = approverAridNo,
                SocietyID = societyId,
                CreatedAt = DateTime.Now
            });
        }

        public static void NotifyEventApprovalRequest(string presidentAridNo, int societyId, string eventId, string eventName)
        {
            var society = SocietyViewModel.GetById(societyId.ToString());
            var mentor = SocietyViewModel.GetMentor(societyId);
            var financePanel = TeacherViewModel.GetFinancePanelMembers(); // List<Student>

            if (mentor != null)
            {
                NotificationViewModel.AddNotification(new Notification
                {
                    Title = "Event Approval Request",
                    Message = $"President has requested approval for event \"{eventName}\".",
                    RecipientAridNo = mentor.AridNo,
                    SenderId = presidentAridNo,
                    SocietyID = societyId,
                    EventId = eventId,
                    CreatedAt = DateTime.Now
                });
            }

            foreach (var finance in financePanel)
            {
                NotificationViewModel.AddNotification(new Notification
                {


                    Title = "Budget Approval Request",
                    Message = $"Event \"{eventName}\" from {society.Name} needs your budget approval.",
                    RecipientAridNo = finance.TeacherID,
                    SenderId = presidentAridNo,
                    SocietyID = societyId,
                    EventId = eventId,
                    CreatedAt = DateTime.Now
                });
            }
        }

        public static void NotifyEventApproved(string approverAridNo, string presidentAridNo, int societyId, string eventId, string eventName)
        {
            NotificationViewModel.AddNotification(new Notification
            {
                Title = "Event Approved",
                Message = $"Your event \"{eventName}\" has been approved.",
                RecipientAridNo = presidentAridNo,
                SenderId = approverAridNo,
                SocietyID = societyId,
                EventId = eventId,
                CreatedAt = DateTime.Now
            });
        }

        public static void NotifyRoleChange(string studentAridNo, string newRole, string changerAridNo, int societyId)
        {
            var society = SocietyViewModel.GetById(societyId.ToString());

            NotificationViewModel.AddNotification(new Notification
            {
                Title = "Role Updated",
                Message = $"Your role in {society.Name} has been changed to \"{newRole}\".",
                RecipientAridNo = studentAridNo,
                SenderId = changerAridNo,
                SocietyID = societyId,
                CreatedAt = DateTime.Now
            });
        }

        public static void NotifyUpcomingEventToAllStudents(string eventName, int societyId, string eventId)
        {
            var society = SocietyViewModel.GetById(societyId.ToString() );

            NotificationViewModel.NotifyAllStudents(new Notification
            {
                Title = "Upcoming Event",
                Message = $"Event \"{eventName}\" is coming soon from {society.Name}.",
                SenderId = "System",
                SocietyID = societyId,
                EventId = eventId,
                CreatedAt = DateTime.Now
            });
        }
    }
}
