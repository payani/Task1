# Task1
Quetion

Write a C# class to execute a number of pieces of work (actions) in the background (i.e.
without blocking the program’s execution).
• The actions must be executed sequentially – one at a time
• The actions must be executed in the order that they were added to the class
• The actions are not necessarily added all at the same time
• The actions are not necessarily all executed on the same thread

solustion

task realated code is implemented in TaskDefine class.

TaskOrganiserThread will execute action after one by one.
TaskOrganiserThread creates new thread for each task and organiser thread will sleep for infinite time.
whenever task list updated call to OnTaskListUpdate() and this method will wakeup the TaskOrganiseThread.
for keeping organizer thread in sleep it will not use the CPU and CPU will free to do some other tasks.
In Action method assume that it is doing some work,i put thread in sleep for random 500 to 1500 milliseconds cosidered as a work.
after every task(Action) completed that thread will invoke a event OnTaskComplete.

run Task1 project and click on Start operation it will create 15 tasks and after completing each task it will display on textblock.
