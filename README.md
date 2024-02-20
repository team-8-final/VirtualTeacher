# .NET Project ({{Project name}})

## Overview

{{Project overview. What users can expect in terms of features.}}

---
<br>
## Features

- Feature 1.
- Feature 2.
- Feature 3.

<br />

## Technologies Used

- Technology 1
- Technology 2
- Technology 3

---

## Installation


Follow these steps to set up and run the application:

1. **Step one**
2. **Step two**
2. **Step three**

<br>

#### Home Page - before and after login

<img></img>

#### Login Page 

<img></img>

{{Pictures representing the different pages of the project and its different states - Browse User (Admin only), API Page, etc.}}

<br />
<img src=  />

## Database Diagram

<img src=https://i.ibb.co/fXQW2NX/db-diagram.png />

<br>

## Solution Structure
The project uses 3 layered structure:

+ Repository layer
+ Service layer
+ Presentation layer and an OpenAPI


```mermaid
flowchart TD
    
    A[OpenAPI] === S
    MVC[MVC] === S[Service Layer]
    S === R[Repository Layer]
    R === D[(Database)]
```

<details>
  <summary>Click to expand/collapse</summary>

  ```
C:.
¦   .gitignore
¦   project_structure.txt
¦   README.md
¦   Virtual Teacher.docx
¦   VirtualTeacher.sln
¦   
+---.github
¦   L---workflows
L---VirtualTeacher
    ¦   appsettings.Development_.json
    ¦   appsettings.json
    ¦   Program.cs
    ¦   VirtualTeacher.csproj
    ¦   VirtualTeacher.csproj.user
    ¦   
    +---Components
    ¦       AssessSubmissionModal.cs
    ¦       Sidebar.cs
    ¦       TeachersModal.cs
    ¦       
    +---Controllers
    ¦   ¦   EmailController.cs
    ¦   ¦   
    ¦   +---API
    ¦   ¦       AccountApiController.cs
    ¦   ¦       ApplicationApiController.cs
    ¦   ¦       CourseApiController.cs
    ¦   ¦       UsersApiController.cs
    ¦   ¦       
    ¦   L---MVC
    ¦           AccountController.cs
    ¦           ApplicationController.cs
    ¦           AssignmentController.cs
    ¦           CourseController.cs
    ¦           HomeController.cs
    ¦           LectureController.cs
    ¦           RatingController.cs
    ¦           SharedController.cs
    ¦           UserController.cs
    ¦           
    +---Data
    ¦       AppDbContext.cs
    ¦       CommentsData.cs
    ¦       CoursesData.cs
    ¦       CourseStudentData.cs
    ¦       CourseTeacherData.cs
    ¦       LecturesData.cs
    ¦       SubmissionsData.cs
    ¦       UsersData.cs
    ¦       
    +---Exceptions
    ¦       DuplicateEntityException.cs
    ¦       EntityNotFoundException.cs
    ¦       InvalidCredentialsException.cs
    ¦       InvalidUserInputException.cs
    ¦       UnauthorizedOperationException.cs
    ¦       
    +---Helpers
    ¦   ¦   MailMapper.cs
    ¦   ¦   ModelMapper.cs
    ¦   ¦   SwaggerTagsFilter.cs
    ¦   ¦   
    ¦   L---CustomAttributes
    ¦           IsAdminAttribute.cs
    ¦           IsTeacherOrAdminAttribute.cs
    ¦           
    +---Migrations
    ¦       20240220074141_initial.cs
    ¦       20240220074141_initial.Designer.cs
    ¦       20240220083328_initial2.cs
    ¦       20240220083328_initial2.Designer.cs
    ¦       20240220083703_hrthr.cs
    ¦       20240220083703_hrthr.Designer.cs
    ¦       20240220095331_asgweinp.cs
    ¦       20240220095331_asgweinp.Designer.cs
    ¦       20240220100321_asgweinpåãâ.cs
    ¦       20240220100321_asgweinpåãâ.Designer.cs
    ¦       AppDbContextModelSnapshot.cs
    ¦       
    +---Models
    ¦   ¦   Comment.cs
    ¦   ¦   Course.cs
    ¦   ¦   Lecture.cs
    ¦   ¦   Note.cs
    ¦   ¦   PaginatedList.cs
    ¦   ¦   Rating.cs
    ¦   ¦   RatingValues.cs
    ¦   ¦   Submission.cs
    ¦   ¦   TeacherApplication.cs
    ¦   ¦   User.cs
    ¦   ¦   
    ¦   +---DTOs
    ¦   ¦   ¦   ApplicationResponseDto.cs
    ¦   ¦   ¦   EmailDto.cs
    ¦   ¦   ¦   LectureTitleIdDto.cs
    ¦   ¦   ¦   
    ¦   ¦   +---Account
    ¦   ¦   ¦       AccountUpdateDto.cs
    ¦   ¦   ¦       CredentialsDto.cs
    ¦   ¦   ¦       UserPasswordDto.cs
    ¦   ¦   ¦       
    ¦   ¦   +---Course
    ¦   ¦   ¦       CommentCreateDto.cs
    ¦   ¦   ¦       CommentResponseDto.cs
    ¦   ¦   ¦       CourseCreateDto.cs
    ¦   ¦   ¦       CourseResponseDto.cs
    ¦   ¦   ¦       CourseUpdateDto.cs
    ¦   ¦   ¦       LectureCreateDto.cs
    ¦   ¦   ¦       LectureResponseDto.cs
    ¦   ¦   ¦       LectureUpdateDto.cs
    ¦   ¦   ¦       RatingCreateDto.cs
    ¦   ¦   ¦       RatingResponseDto.cs
    ¦   ¦   ¦       
    ¦   ¦   L---User
    ¦   ¦           UserCreateDto.cs
    ¦   ¦           UserResponseDto.cs
    ¦   ¦           UserUpdateDto.cs
    ¦   ¦           
    ¦   +---Enums
    ¦   ¦       CourseTopic.cs
    ¦   ¦       UserRole.cs
    ¦   ¦       
    ¦   L---QueryParameters
    ¦           AssignmentsQueryParameters.cs
    ¦           CommentQueryParameters.cs
    ¦           CourseQueryParameters.cs
    ¦           UserQueryParameters.cs
    ¦           
    +---PrivateData
    ¦   +---Assignments
    ¦   ¦   +---course-1
    ¦   ¦   +---course-10
    ¦   ¦   L---course-3
    ¦   ¦           lecture-10.txt
    ¦   ¦           
    ¦   L---Submissions
    ¦       +---course-1
    ¦       ¦   +---lecture-1
    ¦       ¦   ¦       ian_c.txt
    ¦       ¦   ¦       julia_lopez.txt
    ¦       ¦   ¦       
    ¦       ¦   L---lecture-2
    ¦       ¦           ian_c.txt
    ¦       ¦           julia_lopez.txt
    ¦       ¦           
    ¦       L---course-3
    ¦           L---lecture-10
    ¦                   admin.txt
    ¦                   
    +---Properties
    ¦       launchSettings.json
    ¦       serviceDependencies.local.json.user
    ¦       
    +---Repositories
    ¦   ¦   ApplicationRepository.cs
    ¦   ¦   CourseRepository.cs
    ¦   ¦   UserRepository.cs
    ¦   ¦   
    ¦   L---Contracts
    ¦           IApplicationRepository.cs
    ¦           ICourseRepository.cs
    ¦           IUserRepository.cs
    ¦           
    +---Services
    ¦   ¦   AccountService.cs
    ¦   ¦   ApplicationService.cs
    ¦   ¦   CourseService.cs
    ¦   ¦   EmailService.cs
    ¦   ¦   UserService.cs
    ¦   ¦   
    ¦   L---Contracts
    ¦           IAccountService.cs
    ¦           IApplicationService.cs
    ¦           ICourseService.cs
    ¦           IEmailService.cs
    ¦           IUserService.cs
    ¦           
    +---ViewModels
    ¦   ¦   ErrorViewModel.cs
    ¦   ¦   HomeIndexViewModel.cs
    ¦   ¦   
    ¦   +---Account
    ¦   ¦       AccountInfoViewModel.cs
    ¦   ¦       AccountUpdateViewModel.cs
    ¦   ¦       LoginViewModel.cs
    ¦   ¦       PasswordViewModel.cs
    ¦   ¦       RegisterViewModel.cs
    ¦   ¦       
    ¦   +---Assignments
    ¦   ¦       AssignmentsViewModel.cs
    ¦   ¦       
    ¦   +---Courses
    ¦   ¦       CourseCreateViewModel.cs
    ¦   ¦       CoursesListViewModel.cs
    ¦   ¦       CourseUpdateViewModel.cs
    ¦   ¦       
    ¦   +---Lectures
    ¦   ¦       LectureCreateViewModel.cs
    ¦   ¦       LectureUpdateViewModel.cs
    ¦   ¦       
    ¦   L---Users
    ¦           UserIndexViewModel.cs
    ¦           UserUpdateViewModel.cs
    ¦           
    +---Views
    ¦   ¦   _ViewImports.cshtml
    ¦   ¦   _ViewStart.cshtml
    ¦   ¦   
    ¦   +---Account
    ¦   ¦       Index.cshtml
    ¦   ¦       Login.cshtml
    ¦   ¦       Password.cshtml
    ¦   ¦       Register.cshtml
    ¦   ¦       Update.cshtml
    ¦   ¦       
    ¦   +---Assignment
    ¦   ¦       Index.cshtml
    ¦   ¦       
    ¦   +---Course
    ¦   ¦       Create.cshtml
    ¦   ¦       Details.cshtml
    ¦   ¦       Index.cshtml
    ¦   ¦       Update.cshtml
    ¦   ¦       
    ¦   +---Home
    ¦   ¦       Index.cshtml
    ¦   ¦       Privacy.cshtml
    ¦   ¦       
    ¦   +---Lecture
    ¦   ¦       Create.cshtml
    ¦   ¦       Details.cshtml
    ¦   ¦       Update.cshtml
    ¦   ¦       
    ¦   +---Shared
    ¦   ¦   ¦   Error.cshtml
    ¦   ¦   ¦   Success.cshtml
    ¦   ¦   ¦   _DisplayRating.cshtml
    ¦   ¦   ¦   _InteractiveRating.cshtml
    ¦   ¦   ¦   _Layout.cshtml
    ¦   ¦   ¦   _Layout.cshtml.css
    ¦   ¦   ¦   _ValidationScriptsPartial.cshtml
    ¦   ¦   ¦   
    ¦   ¦   L---Components
    ¦   ¦       +---Sidebar
    ¦   ¦       ¦       Index.cshtml
    ¦   ¦       ¦       
    ¦   ¦       L---TeachersModal
    ¦   ¦               Index.cshtml
    ¦   ¦               
    ¦   L---User
    ¦           Index.cshtml
    ¦           Update.cshtml
    ¦           
    L---wwwroot
        ¦   favicon.ico
        ¦   
        +---css
        ¦       site.css
        ¦       
        +---images
        ¦   ¦   avatar-default.jpg
        ¦   ¦   home-temp.png
        ¦   ¦   
        ¦   +---avatars
        ¦   ¦       admin.jpg
        ¦   ¦       brownA.jpg
        ¦   ¦       johndoe.jpg
        ¦   ¦       
        ¦   L---flags
        ¦       ¦   English.jpg
        ¦       ¦   French.jpg
        ¦       ¦   German.jpg
        ¦       ¦   Japanese.jpg
        ¦       ¦   Russian.jpg
        ¦       ¦   Spanish.jpg
        ¦       ¦   
        ¦       L---svgFlags
        ¦               Bulgarian.svg
        ¦               Chinese.svg
        ¦               English.svg
        ¦               French.svg
        ¦               German.svg
        ¦               Japanese.svg
        ¦               Russian.svg
        ¦               Spanish.svg
        ¦               
        +---js
        ¦       site.js
        ¦       
        L---lib
            +---bootstrap
            ¦   ¦   LICENSE
            ¦   ¦   
            ¦   L---dist
            ¦       +---css
            ¦       ¦       bootstrap-grid.css
            ¦       ¦       bootstrap-grid.css.map
            ¦       ¦       bootstrap-grid.min.css
            ¦       ¦       bootstrap-grid.min.css.map
            ¦       ¦       bootstrap-grid.rtl.css
            ¦       ¦       bootstrap-grid.rtl.css.map
            ¦       ¦       bootstrap-grid.rtl.min.css
            ¦       ¦       bootstrap-grid.rtl.min.css.map
            ¦       ¦       bootstrap-reboot.css
            ¦       ¦       bootstrap-reboot.css.map
            ¦       ¦       bootstrap-reboot.min.css
            ¦       ¦       bootstrap-reboot.min.css.map
            ¦       ¦       bootstrap-reboot.rtl.css
            ¦       ¦       bootstrap-reboot.rtl.css.map
            ¦       ¦       bootstrap-reboot.rtl.min.css
            ¦       ¦       bootstrap-reboot.rtl.min.css.map
            ¦       ¦       bootstrap-utilities.css
            ¦       ¦       bootstrap-utilities.css.map
            ¦       ¦       bootstrap-utilities.min.css
            ¦       ¦       bootstrap-utilities.min.css.map
            ¦       ¦       bootstrap-utilities.rtl.css
            ¦       ¦       bootstrap-utilities.rtl.css.map
            ¦       ¦       bootstrap-utilities.rtl.min.css
            ¦       ¦       bootstrap-utilities.rtl.min.css.map
            ¦       ¦       bootstrap.css
            ¦       ¦       bootstrap.css.map
            ¦       ¦       bootstrap.min.css
            ¦       ¦       bootstrap.min.css.map
            ¦       ¦       bootstrap.rtl.css
            ¦       ¦       bootstrap.rtl.css.map
            ¦       ¦       bootstrap.rtl.min.css
            ¦       ¦       bootstrap.rtl.min.css.map
            ¦       ¦       
            ¦       L---js
            ¦               bootstrap.bundle.js
            ¦               bootstrap.bundle.js.map
            ¦               bootstrap.bundle.min.js
            ¦               bootstrap.bundle.min.js.map
            ¦               bootstrap.esm.js
            ¦               bootstrap.esm.js.map
            ¦               bootstrap.esm.min.js
            ¦               bootstrap.esm.min.js.map
            ¦               bootstrap.js
            ¦               bootstrap.js.map
            ¦               bootstrap.min.js
            ¦               bootstrap.min.js.map
            ¦               
            +---font-awesome
            ¦   +---css
            ¦   ¦       all.min.css
            ¦   ¦       
            ¦   L---webfonts
            ¦           fa-brands-400.ttf
            ¦           fa-brands-400.woff2
            ¦           fa-regular-400.ttf
            ¦           fa-regular-400.woff2
            ¦           fa-solid-900.ttf
            ¦           fa-solid-900.woff2
            ¦           fa-v4compatibility.ttf
            ¦           fa-v4compatibility.woff2
            ¦           
            +---jquery
            ¦   ¦   LICENSE.txt
            ¦   ¦   
            ¦   L---dist
            ¦           jquery.js
            ¦           jquery.min.js
            ¦           jquery.min.map
            ¦           
            +---jquery-validation
            ¦   ¦   LICENSE.md
            ¦   ¦   
            ¦   L---dist
            ¦           additional-methods.js
            ¦           additional-methods.min.js
            ¦           jquery.validate.js
            ¦           jquery.validate.min.js
            ¦           
            L---jquery-validation-unobtrusive
                    jquery.validate.unobtrusive.js
                    jquery.validate.unobtrusive.min.js
                    LICENSE.txt
  
  ```
</details>


---

## Contributors
For further information, please feel free to contact us:

| Authors                   | Emails                                                      | GitHub                                      |
| ------                    | ------                                                      | ------                                      |
| Stanimir Nenkov           | staanimir@gmail.com                        	              | [link](https://github.com/stnmrr)           |
| Tomi Tsolov               | tvtsolov@gmail.com                                          | [link](https://github.com/tvtsolov)         |
| George Halachev           | george.halachev@gmail.com                                   | [link](https://github.com/george-halachev)  |

---
