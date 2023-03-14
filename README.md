# StudentAPI
An API written in ASP.NET Core 6.0 using the MongoDB non-relational database
## What contains
### For students
1) Returns all students whose exam marks are higher than the specified
2) Returns all students who took the exam to the specified teacher
3) Returns all students of the specified group
4) Returns student by specified id (int/string)
5) Add/update/delete student
### For stud group
1) Returns groups in which there are more men than women
2) Returns groups that have students who have passed at least one exam for 25 (3)
3) Returns group that contains the specified student
4) Returns group by specified id (string)
5) Returns groups by course or direction
6) Add/update/delete stud group
### For exams
1) Returns all exams that took place before the specified date
2) Returns all exams with grade >= 35 (4)
3) Returns all exams with grade < 35 (4)
4) Returns exam by specified id (string)
5) Returns exams by discipline name
6) Add/update/delete exam
### For lecturers
1) Returns lecturers whose stage is higher than the specified
2) Returns lecturers who took the exam for the specified student
3) Returns lecturers by specified department
4) Returns lecturer by specified id (int/string)
5) Add/update/delete lecturer
