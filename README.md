# Description
This project is a Student-Academic Management System designed to teach the processes of multi-layered web application development. The system allows students and academics to perform the following actions:

Description
This project is a Student-Academic Management System designed to teach the processes of multi-layered web application development. The system allows students and academics to perform the following actions:
•	Update profile information
•	Select courses
•	View selected courses
•	Add courses
It also includes features such as database interaction via an API and the use of a multi-layered architecture.
# Features
o	**Login and Logout:** Users can log in and log out of the system.

o	**Profile Management:** Users can view and update their profile information.

o	**Course Selection:** Students can browse available courses and enroll.

o	**Database Integration:** Interacts with the database through an API.

o	**Multi-Layered Architecture:** The application is divided into data access, business logic, and user interface layers (Model-Controller-View, also known as MVC).
   
# Database with SQL Server
## Tables
### 1.	Course
o	**CourseID (Primary Key):** Unique identifier for each course.

o	**CourseName:** Name of the course.

o	**Credits:** Number of credits the course is worth.

o	**InstructorID (Foreign Key):** Links to the Instructor table to identify the instructor teaching the course.

o	**Explanation:** Description of the course.

### 2.	Instructor
o	**InstructorID (Primary Key):** Unique identifier for each instructor.

o	**FirstName:** First name of the instructor.

o	**LastName:** Last name of the instructor.

o	**Email:** Contact email of the instructor.

o	**Department:** Department in which the instructor works.

o	**Password:** Password for the instructor’s account.

### 3.	Student_Courses 
o	**StudentID (Composite Key):** Links to the Student table to identify the student selecting the course.

o	**CourseID (Composite Key):** Links to the Course table to identify the selected course.

### 4.	Student
o	**StudentID (Primary Key):** Unique identifier for each student.

o	**First_Name:** First name of the student.

o	**Last_Name:** Last name of the student.

o	**E-mail:** Contact email of the student.

o	**Password:** Password for the student’s account.

o	**Major:** The student's major or field of study.

# Controller
## Home
**GET:** /Home/INDEX

**POST:** /Home/SUBMİTFORM

**GET:** /Home/SifreSifirla

**POST:** /Home/SfireSifirla

## Instructor
**GET:** /Instructor/AkademisyenSayfasi

**GET:** /Instructor/ProfilGuncelle/{id}

**POST:** /Instructor/ProfilGuncelle/{id}

**GET:** /Instructor/DersEkleme/{id}

**POST:** /Instructor/DersEkleme/{id}

## Student
**GET:** /Student/OgrenciSayfasi

**GET:** /Student/ProfilGuncelle/{id}

**POST:** /Student/ProfilGuncelle/{id}

**GET:** /Student/DersSecim/{id}

**POST:** /Student/DersSecim/{id}

**GET:** /Student/DersGoruntule/{studentId}

**GET:** /Student/DersDetay/{courseId}

# Pages
The application uses Razor Pages to manage user interactions and display dynamic content. Razor Pages are designed to make page-focused scenarios easier and more efficient. Each page is represented by a .cshtml file and its associated page model class, which contains the logic needed to handle requests and responses.

## Key Razor Pages
### Home
o **Index Page:** This page allows users to log in.

o **SifreSifirla Page:** This page enables users to change their password.
### Student

o **OgrenciSayfasi Page:** Enables students to view and update their personal information.

o **DersSecim Page:** Allows students to browse and enroll in available courses.

o **ProfilGuncelle Page:** Enables students to update their information.

o **DersGoruntule Page:** Allows students to view the courses they have selected.

o **DersDetay Page:** Enables students to view the details of their courses.

### Instructor

o **AkademisyenSayfasi Page:** Enables instructors to view and update their personal information.

o **DersEkleme Page:** Allows instructors to add new courses.

o **ProfilGuncelle Page:** Enables instructors to update their information.

# Technologies

o **Backend:** ASP.NET Core

o **Frontend:** HTML, CSS, JavaScript

o **Database:** SQL Server

o **ORM:** Entity Framework Core

o **Testing:** Postman

# Contributor
***Rahim Kaan Kacar***

