using NUnit.Framework;
using HealthTrackerApp.BusinessLogic.Managers;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;
using System;

namespace HealthTrackerApp.Tests
{
    // Tests for GoalManager class
    // Covers delegates, events, and goal progress calculations
    [TestFixture]
    public class GoalManagerTests
    {
        // Goal Progress Calculation Tests

        [Test]
        public void GoalManager_WeightLossGoal_CalculatesProgressCorrectly()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightLoss,
                InitialValue = 80.0,  // Started at 80kg
                TargetValue = 75.0,   // Target is 75kg
                CurrentValue = 77.0,  // Currently at 77kg
                StartDate = DateTime.Now,
                TargetDate = DateTime.Now.AddMonths(3)
            };

            // Act
            double progress = manager.CalculateProgress(goal);

            // Assert - lost 3kg out of 5kg = 60% progress
            Assert.That(progress, Is.EqualTo(60.0).Within(0.1));
        }

        [Test]
        public void GoalManager_WeightGainGoal_CalculatesProgressCorrectly()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightGain,
                InitialValue = 70.0,  // Started at 70kg
                TargetValue = 75.0,   // Target is 75kg
                CurrentValue = 72.0,  // Currently at 72kg
                StartDate = DateTime.Now,
                TargetDate = DateTime.Now.AddMonths(3)
            };

            // Act
            double progress = manager.CalculateProgress(goal);

            // Assert - gained 2kg out of 5kg = 40% progress
            Assert.That(progress, Is.EqualTo(40.0).Within(0.1));
        }

        [Test]
        public void GoalManager_ExerciseFrequencyGoal_CalculatesProgressCorrectly()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.ExerciseFrequency,
                InitialValue = 0.0,   // Starting from zero
                TargetValue = 20.0,   // Target 20 workouts
                CurrentValue = 8.0,   // Completed 8 workouts
                StartDate = DateTime.Now,
                TargetDate = DateTime.Now.AddMonths(1)
            };

            // Act
            double progress = manager.CalculateProgress(goal);

            // Assert - 8 out of 20 = 40% progress
            Assert.That(progress, Is.EqualTo(40.0).Within(0.1));
        }

        // Goal Achievement Tests

        [Test]
        public void GoalManager_WeightLossAchieved_ReturnsTrue()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightLoss,
                InitialValue = 80.0,
                TargetValue = 75.0,
                CurrentValue = 74.0,  // Below target!
                StartDate = DateTime.Now.AddMonths(-2),
                TargetDate = DateTime.Now.AddMonths(1)
            };

            // Act
            bool isAchieved = manager.IsGoalAchieved(goal);

            // Assert - current weight is below target
            Assert.That(isAchieved, Is.True);
        }

        [Test]
        public void GoalManager_WeightGainNotAchieved_ReturnsFalse()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightGain,
                InitialValue = 70.0,
                TargetValue = 75.0,
                CurrentValue = 72.0,  // Still below target
                StartDate = DateTime.Now.AddMonths(-1),
                TargetDate = DateTime.Now.AddMonths(2)
            };

            // Act
            bool isAchieved = manager.IsGoalAchieved(goal);

            // Assert - hasn't reached target yet
            Assert.That(isAchieved, Is.False);
        }

        // Delegate and Event Tests

        [Test]
        public void GoalManager_GoalAchieved_FiresEvent()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightLoss,
                InitialValue = 80.0,
                TargetValue = 75.0,
                CurrentValue = 76.0,  // Not achieved yet
                IsCompleted = false,
                StartDate = DateTime.Now.AddMonths(-2),
                TargetDate = DateTime.Now.AddMonths(1)
            };

            bool eventFired = false;
            string eventMessage = null;

            // Subscribe to event
            manager.OnGoalAchieved += (g, msg) =>
            {
                eventFired = true;
                eventMessage = msg;
            };

            // Act - update progress to achieve goal
            manager.UpdateProgress(goal, 74.0);

            // Assert - event should fire
            Assert.That(eventFired, Is.True);
            Assert.That(eventMessage, Is.Not.Null);
            Assert.That(eventMessage.ToLower(), Does.Contain("achieved"));
            Assert.That(goal.IsCompleted, Is.True);
        }

        [Test]
        public void GoalManager_PartialProgress_DoesNotFireAchievedEvent()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightLoss,
                InitialValue = 80.0,
                TargetValue = 75.0,
                CurrentValue = 78.0,
                IsCompleted = false,
                StartDate = DateTime.Now,
                TargetDate = DateTime.Now.AddMonths(3)
            };

            bool achievedEventFired = false;
            manager.OnGoalAchieved += (g, msg) => achievedEventFired = true;

            // Act - update but not to achievement level
            manager.UpdateProgress(goal, 77.0);

            // Assert - achievement event should NOT fire
            Assert.That(achievedEventFired, Is.False);
            Assert.That(goal.IsCompleted, Is.False);
        }

        [Test]
        public void GoalManager_ProgressUpdate_FiresProgressEvent()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightLoss,
                InitialValue = 80.0,
                TargetValue = 75.0,
                CurrentValue = 78.0,
                StartDate = DateTime.Now,
                TargetDate = DateTime.Now.AddMonths(3)
            };

            bool progressEventFired = false;
            double capturedProgress = 0;

            // Subscribe to progress event
            manager.OnGoalProgress += (g, progress) =>
            {
                progressEventFired = true;
                capturedProgress = progress;
            };

            // Act
            manager.UpdateProgress(goal, 77.0);

            // Assert - progress event should fire
            Assert.That(progressEventFired, Is.True);
            Assert.That(capturedProgress, Is.GreaterThan(0));
            Assert.That(capturedProgress, Is.LessThanOrEqualTo(100));
        }

        // Motivation Message Tests

        [Test]
        public void GoalManager_HighProgress_ReturnsEncouragingMessage()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightLoss,
                InitialValue = 80.0,
                TargetValue = 75.0,
                CurrentValue = 75.5,  // 90% progress
                IsCompleted = false,
                StartDate = DateTime.Now.AddMonths(-2),
                TargetDate = DateTime.Now.AddMonths(1)
            };

            // Act
            string message = manager.GetMotivationMessage(goal);

            // Assert - should be encouraging for high progress
            Assert.That(message, Is.Not.Empty);
            Assert.That(message, Does.Contain("Almost"));
        }

        [Test]
        public void GoalManager_CompletedGoal_ReturnsCongratulatoryMessage()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightLoss,
                InitialValue = 80.0,
                TargetValue = 75.0,
                CurrentValue = 74.0,
                IsCompleted = true,
                CompletedDate = DateTime.Now,
                StartDate = DateTime.Now.AddMonths(-3),
                TargetDate = DateTime.Now
            };

            // Act
            string message = manager.GetMotivationMessage(goal);

            // Assert - should congratulate
            Assert.That(message, Is.Not.Empty);
            Assert.That(message.ToLower(), Does.Contain("achieved"));
        }

        // Edge Case Tests

        [Test]
        public void GoalManager_ZeroProgress_ReturnsStartingMessage()
        {
            // Arrange
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightGain,
                InitialValue = 70.0,
                TargetValue = 75.0,
                CurrentValue = 70.0,  // No progress yet
                StartDate = DateTime.Now,
                TargetDate = DateTime.Now.AddMonths(3)
            };

            // Act
            double progress = manager.CalculateProgress(goal);
            string message = manager.GetMotivationMessage(goal);

            // Assert - should show 0% progress
            Assert.That(progress, Is.EqualTo(0.0).Within(0.1));
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void GoalManager_ExceedTarget_Returns100Percent()
        {
            // Arrange - exceeded weight loss target
            var manager = new GoalManager();
            var goal = new Goal
            {
                GoalType = GoalType.WeightLoss,
                InitialValue = 80.0,
                TargetValue = 75.0,
                CurrentValue = 70.0,  // Lost more than target!
                StartDate = DateTime.Now.AddMonths(-3),
                TargetDate = DateTime.Now.AddMonths(1)
            };

            // Act
            double progress = manager.CalculateProgress(goal);

            // Assert - should cap at 100%
            Assert.That(progress, Is.EqualTo(100.0).Within(0.1));
        }
    }
}