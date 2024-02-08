import pickle

class Goal:
    def __init__(self, name):
        self.name = name
        self.completed = False
    
    def mark_completed(self):
        self.completed = True
    
    def display_status(self):
        if self.completed:
            return "[X]"
        else:
            return "[ ]"

class SimpleGoal(Goal):
    def __init__(self, name, points):
        super().__init__(name)
        self.points = points
    
    def record_event(self):
        self.mark_completed()
        return self.points

class EternalGoal(Goal):
    def __init__(self, name, points):
        super().__init__(name)
        self.points = points
    
    def record_event(self):
        return self.points

class ChecklistGoal(Goal):
    def __init__(self, name, points_per_completion, target_count, bonus_points):
        super().__init__(name)
        self.points_per_completion = points_per_completion
        self.target_count = target_count
        self.completed_count = 0
        self.bonus_points = bonus_points
    
    def record_event(self):
        self.completed_count += 1
        if self.completed_count == self.target_count:
            self.mark_completed()
            return self.points_per_completion * self.target_count + self.bonus_points
        else:
            return self.points_per_completion
    
    def display_status(self):
        if self.completed:
            return f"[X] Completed {self.completed_count}/{self.target_count} times"
        else:
            return f"[ ] Completed {self.completed_count}/{self.target_count} times"

class EternalQuest:
    def __init__(self):
        self.goals = []
        self.score = 0
    
    def add_goal(self, goal):
        self.goals.append(goal)
    
    def record_event(self, goal_index):
        points_earned = self.goals[goal_index].record_event()
        self.score += points_earned
        return points_earned
    
    def display_goals(self):
        for i, goal in enumerate(self.goals):
            print(f"{i+1}. {goal.name} - {goal.display_status()}")
    
    def save_progress(self, filename):
        with open(filename, 'wb') as file:
            pickle.dump(self, file)
    
    @staticmethod
    def load_progress(filename):
        with open(filename, 'rb') as file:
            return pickle.load(file)

if __name__ == "__main__":
    eternal_quest = EternalQuest()

    marathon_goal = SimpleGoal("Run a Marathon", 1000)
    scriptures_goal = EternalGoal("Read Scriptures", 100)
    temple_goal = ChecklistGoal("Attend Temple", 50, 10, 500)

    eternal_quest.add_goal(marathon_goal)
    eternal_quest.add_goal(scriptures_goal)
    eternal_quest.add_goal(temple_goal)


    eternal_quest.record_event(0) 
    eternal_quest.record_event(1)
    eternal_quest.record_event(2) 
    eternal_quest.record_event(2) 

    eternal_quest.display_goals()
    print("Score:", eternal_quest.score)

    eternal_quest.save_progress("progress.pkl")

    loaded_eternal_quest = EternalQuest.load_progress("progress.pkl")
    print("\nLoaded goals and score:")
    loaded_eternal_quest.display_goals()
    print("Loaded Score:", loaded_eternal_quest.score)
