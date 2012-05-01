# Web Application Development (CE00953-5) Assignment

## To be completed individually

* Design hand in (paper copy only): **Friday 2nd March 2012**
* Web application demonstrations: **Tuesday 1st May 2012**
* Report hand-in (including a digital copy): **Friday 27th April 2012**

**All hand-ins will be made directly to the Octagon reception.**

**No hand-in is required for the demonstration section of the assignment.**

## DVD Rental Site

You have been asked to develop a prototype website for a DVD rental company.

###Visitors/Customers

#### Browsing/Renting a DVD

The kind of functionality required for members/visitors is as follows (see marking criteria for more exact instructions):

* <del>Browse details on various categories of rentable DVD s (Action & Adventure, Animation, Comedy, Documentary, Drama, etc.)</del>
* <del>Keyword search for DVD titles</del>
* <del>View DVD of the week</del>
* <del>Add a DVD to a personal rental list (requires log in)</del>
* <ins>Change the order of DVD s in personal rental list (requires log in)</ins>
* <del>Remove a DVD from a personal rental list (requires log in)</del>
* <del>View a personal history of DVD rentals including the dates when the DVD s were rented (requires log in)</del>
* <del>Rate a DVD that has been rented or is currently rented (requires log in)</del>
* <del>Post comments on a DVD that has been rented or is currently rented  (requires log in)</del>
* <del>The ability for a member to edit (only) their own comments (requires log in)</del>
* <del>View comments that other customers have made</del>
* <del>Flag abusive or inappropriate comments for the administrator's attention</del>
* <del>Log out</del>

#### Registering for the Site

Visitors should be able to:

* <del>Complete an online form to register as a user of the website   this could include personal details such name, username and contact details (address and telephone number(s))</del>
* <del>Check to see if the username they want is available</del>
* <del>Have access to the site once the registration form has been successfully completed</del>

### Administrator

The kind of functionality required for the administrator to perform is as follows (see marking criteria for more exact instructions):

* <del>Add the details of new DVD s for rental (including date for when the DVD becomes available to rent   you could have a default of the date when the record was added)</del>
* <del>Edit details of DVD s that are currently in stock</del>
* <del>Remove DVD s from being available to rent (the details of the DVD should still remain on the system)</del>
* <del>Post DVD of the week (available to all users including visitors)</del>
* Register the return of a DVD (this should automatically be removed as the  currently rented  DVD from the account of the member who rented it)
* Register dispatch of the next available DVD on the rental list (as a simulation this could be any DVD on the list)
* <del>Investigate comments flagged by members</del>
* <del>Moderate (potentially remove) comments made by members</del>
* <del>Remove or suspend members from the site</del>
* <del>Message individual members (picked up the next time the member logs in)</del>
* <del>Message all members (picked up on next log in)</del>

### General

Your application will be a demonstration of your ability at programming using ASP .NET while taking issues relating to web design and development into account. The relevant lectures and tutorials will help you to do this but you will also be expected to actively develop your own understanding and skills.

### Your Task

Develop a web application with sufficient functionality to accommodate the tasks set out in the scenario. In doing so you must also:

* Incorporate appropriate use(s) of Ajax
* Incorporate member authentication and authorisation
* Ensure your site conforms to current web standards (CSS and XHTML), and that your site is available across multiple browsers
* Develop your site using ASP .NET.

#### Section 1   Design (10%)

You are expected to produce design documents for the above web application as follows:

* Appropriate design documentation as shown in the lecture/module learning material
* Database design

**You will hand your design documents in to the office (paper copies only).**

*It is not expected that your finished web application will exactly match your design documents. They should, however, show a full engagement with the assignment criteria and you should therefore strive to make it possible to build your web application based upon the design documentation.*

#### Section 2   Implementation (50%)

This will be assessed in a 15 minute demonstration

During your demonstration you are required to demonstrate the functionality of your implemented web application

#### Section 3 - Report (40%)

In addition to the paper-copy that you hand-in you should also hand-in a CD containing a digital copy of the work that you ve produced for this section of the assignment.

#### Part 1   Discussion Section

You will be expected to produce a **1500 (approx.)** word referenced report on the following:
	
	When developing and deploying web applications you will face a number of technical issues that you will have to overcome.
	These issues include performance, security, and configuration and maintenance.
	Investigate and discuss these issues in terms of the problems that they may present to you as a developer and describe the methods by which ASP.NET addresses them.

#### Part 2   Functional Testing

You will also be expected to hand in (not part of the word count):

* Documentation showing functional testing of your application using test cases (including test data and results)

## Learning Outcomes

* Design and implement a web application for use on multiple browsers, including connection to a well-designed database and using server and client side scripting applying web standards.
* Test a web application using current techniques and document any test results.
* Discuss the technical issues in web applications, and the problems that could occur when implementing in the real world.

## Marking Criteria   Design (10%)

<table border>
	<tr>
		<td>Element</td>
		<td>Mark (out of)</td>
    </tr>
	<tr>
		<td>Web design documentation</td>
		<td>100</td>
	</tr>
	<tr>
		<td>Database design</td>
		<td>100</td>
	</tr>
</table> 

The criteria below should be read in the context of the methods you were shown as part of the module.

### 70 - 100

The web design documentation will exhibit an exhaustive level of detail with an excellent level of clarity and conciseness. Database designs will be clear and accurate showing a high level of knowledge, analysis and application.

### 60 - 69

The web design documentation will exhibit a high level of detail with a high level of clarity and conciseness. Database designs will be clear and accurate showing a good level of knowledge, analysis and application.

### 50 - 59

The web design documentation will exhibit good detail in a clearly presented manner. Database designs will be clear and have a high level of accuracy showing evidence of knowledge, analysis and application.

### 40 - 49

The web design documentation will exhibit enough detail and with sufficient clarity to communicate the basic design of the web application. Database designs will provide evidence of an understanding of the data structures required to satisfy the requirements of the assignment, showing evidence of knowledge and application.

## Marking Criteria   Implementation (50%)

### Base Functionality

The base functionality is worth 40% of the marks available for the implementation section of the assignment.

#### General

<table border>
	<tr>
		<td>Functionality</td>
		<td>Mark (out of)</td>
    </tr>
	<tr>
		<td>XHTML validated (strict)</td>
		<td>1</td>
	</tr>
	<tr>
		<td>CSS validated</td>
		<td>1</td>
	</tr>
</table>

#### User/Member

<table border>
	<tr>
		<td>Functionality</td>
		<td>Mark (out of)</td>
    </tr>
	<tr>
		<td>Register as a member of the website</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Check to see if username is available</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Automatic login when registration complete</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Browse DVD s by category</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Search DVD s by a single field</td>
		<td>1</td>
	</tr>
	<tr>
		<td>View DVD of the week</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Add a DVD to your rental list (must be logged in)</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Remove a DVD from your rental list</td>
		<td>1</td>
	</tr>
	<tr>
		<td>View DVD rental history</td>
		<td>1</td>
	</tr>
	<tr>
		<td>View dates DVD s were rented in rental history</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Log out</td>
		<td>1</td>
	</tr>
</table>
	 
#### Administrator

<table border>
	<tr>
		<td>Functionality</td>
		<td>Mark (out of)</td>
    </tr>
	<tr>
		<td>Register new DVD for rental</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Edit details of DVD in stock</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Remove DVD from stock (details remain)</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Register return of DVD</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Register dispatch of next DVD</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Remove comments made by members</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Suspend members from the site</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Remove members from the site</td>
		<td>1</td>
	</tr>
</table>
 
### Additional Functionality

The additional functionality is worth an additional 40% of the marks available for the implementation section of the assignment.

#### User/Member

<table border>
	<tr>
		<td>Functionality</td>
		<td>Mark (out of)</td>
    </tr>
	<tr>
		<td>Add personal details when registering</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Search DVD s by multiple fields simultaneously</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Change the order of DVD s on your rental list</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Rate a DVD (logged in - previous rental only)</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Post comments on DVD (logged in - previous rental only)</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Edit comments on DVD (logged in - own comments only)</td>
		<td>1</td>
	</tr>
	<tr>
		<td>View comments on a DVD</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Flag comment for moderation</td>
		<td>1</td>
	</tr>
</table>
 
#### Administrator

<table border>
	<tr>
		<td>Functionality</td>
		<td>Mark (out of)</td>
    </tr>
	<tr>
		<td>Post DVD of the week available to all</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Set DVD availability date</td>
		<td>1</td>
	</tr>
	<tr>
		<td>View flagged comments</td>
		<td>1</td>
	</tr>
	<tr>
		<td>View flagged comments in context</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Message individual members (picked up on next log in)</td>
		<td>1</td>
	</tr>
	<tr>
		<td>Message all members (picked up on next log in)</td>
		<td>1</td>
	</tr>

	<tr>
		<td></td>
		<td></td>
	</tr>
</table>

### Discretionary Marks

The discretionary marks are worth an additional 20% of the marks available for the implementation section of the assignment.

<table border>
	<tr>
		<td>Element</td>
		<td>Mark (out of)</td>
    </tr>
	<tr>
		<td>Use of server side programming</td>
		<td>100</td>
	</tr>
	<tr>
		<td>Use(s) of Ajax</td>
		<td>100</td>
	</tr>
</table>

#### 70 - 100

* Use of server-side programming will exhibit a high level of knowledge, analysis and application based on substantial independent research.
* Use of Ajax will exhibit a high level of knowledge, analysis and application based on substantial independent research.

#### 60 - 69

* Use of server-side programming will exhibit a good level of knowledge, analysis and application based on some independent research. 
* Use of Ajax will exhibit a good level of knowledge, analysis and application based on some independent research. 

#### 50 - 59

* Use of server-side programming will exhibit evidence of knowledge, analysis and application based on an understanding of the subject area.
* Use of Ajax will exhibit evidence of knowledge, analysis and application based on an understanding of the subject area.

#### 40 - 49
* Use of server-side programming will exhibit evidence of knowledge and application based on material provided as part of the module.	
* Use of Ajax will exhibit evidence of knowledge and application based on material provided as part of the module.	

## Marking Criteria   Report 

### Part 1 - Evaluation (35%)

<table border>
	<tr>
		<td>Report Element</td>
		<td>Mark (out of)</td>
		<td>Weighting</td>
	</tr>
	<tr>
		<td>Introduction - What the report is about, why it is important, how it is organised.</td>
		<td>100</td>
		<td>5%</td>
	</tr>
	<tr>
		<td>Main Body - Investigation of security, performance and configuration and maintenance when developing and maintaining web applications.<br/>Description of how ASP.NET addresses them.</td>
		<td>100</td>
		<td>18%</td>
	</tr>
	<tr>
		<td>Conclusion - Summary of the main findings, including a substantiated assessment of the effectiveness of ASP.NET in addressing the issues described.</td>
		<td>100</td>
		<td>8%</td>
	</tr>
	<tr>
		<td>Quality of references (Harvard referencing will be used).</td>
		<td>100</td>
		<td>4%</td>
	</tr>
	<tr>
		<td>Total for the report</td>
		<td>-</td>
		<td>35%</td>
	</tr>
</table>
 
#### 70 - 100

* The introduction will be clear and concise in stating what the report is about and why the topic is important within the general context of web application development and maintenance. The introduction should indicate the main areas of discussion as a  general overview  of what is to be written within this context. 
* The main body will be clear and concise in justifying the need to address performance, security, and configuration and maintenance when developing and maintaining web applications. The description of ASP.NET s approach to addressing these issues will be clear, concise and strictly to the point.  A high level of reading in the area will be exhibited with all sources correctly referenced.
* The conclusion will be clear and concise in summarising the main findings/discussion points. A conclusion will be reached upon the ability and effectiveness with which ASP.NET addresses the issues under discussion. Justification of this conclusion will be based solely upon content already introduced in the body of the report.
* References will be peer-reviewed where appropriate and will show quality and depth of reading in the area. 

#### 60 - 69

* The introduction will be clear in stating what the report is about and why the topic is important within the general context of web application development and maintenance. The main areas of discussion will be outlined.
* The main body will be clear and concise in describing the need to address performance, security, and configuration and maintenance when developing and maintaining web applications. The description of ASP.NET s approach to addressing these issues will be clear and concise. Reading in the area will be exhibited with all sources correctly referenced.
* The conclusion will be clear and concise in summarising the main findings/discussion points. A conclusion will be reached upon the ability and effectiveness of ASP.NET. Justification of this conclusion will primarily draw upon content already introduced in the body of the report.
* References will be peer-reviewed where appropriate and will show reading in the area. 

#### 50 - 59

* The introduction will state what the report is about and why it is important within the general context of web application development.
* The main body will outline the need to address performance, security, and configuration and maintenance when developing and maintaining web applications. The description of ASP.NET s approach to addressing these issues will be clear. Reading in the area will be exhibited with sources referenced.
* The conclusion will summarise the report findings. A conclusion will be reached about the ability and effectiveness of ASP.NET.
* References will be of good quality and will show reading in the area. 

#### 40 - 49
* The introduction will state what the report is about.
* The main body will discuss ASP.NET in terms of developing and maintaining web applications. There will be a general description of the approach of ASP.NET to the issues under discussion. The discussion should show evidence of reading in the area with all sources referenced.
* The conclusion will summarise the report findings and a conclusion on the usefulness of ASP.NET will be reached.
* References will be correctly used and will show reading in the area. 

### Part 2   Testing (5%)

<table border>
	<tr>
		<td>Element</td>
		<td>Mark (out of)</td>
	</tr>
	<tr>
		<td>Functional testing</td>
		<td>100</td>
	</tr>
</table>

#### 70 - 100

Exhaustive testing will have been planned and carried out.

#### 60 - 69

Testing on all functionality will have been planned and carried out.

#### 50 - 59

A good level of testing will have been planned and carried out. 

#### 40 - 49

Testing has been done well where carried out. 

**The University Regulations regarding extenuating circumstances and plagiarism will apply.**