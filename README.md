Software Engineer Assignment at Webbing 
 
We are a Mobile Virtual Network Operator and receive a continuous stream of events from our SIM cards called NetworkEvents (NE).
 
Each event is defined by four properties: SIM identifier, session identifier, quota (amount of data usage in megabytes), and createdOnUtc (timestamp of the event).
In the scope of the assignment, you will be asked to create two reports: usage by sim, and usage by customer.
Usage-by-customer: This should be the sum of usage consumed by each customer in a range of dates.
Usage-by-sim: This should be the sum of usage consumed by each sim in a range of dates.
A sim can have multiple Network Events on the same day and for the same session therefore, you should store it with a minimum resolution of one day.
Please take into consideration the following points when providing your solution:
1. We are receiving millions of events per hour, so the solution must be capable of handling a high volume of data.
2. Scalability is crucial to accommodate our growing data processing needs.
3. The solution must ensure that data retrieval is performed with minimal delay, ideally in real-time.
 
TASK
1. Architecture – A diagram or a readme which explains your solution.
2. POC (Backend) – Implement your solution in the attached code base, see below.
3. UI (Frontend) – see below.
 
CODE BASE
The Solution consists of 2 main projects -
The first one is the API, which has two controllers, admin and usage.
You do not have to modify the admin controller it exists only to seed the DB and to show you the existing data (sims, customers and network events). The usage controller is one of your possible entry points (for testing your API implementation).
The second project is the service itself where the entities are defined and where the connection to the database lives.
You do not have to modify the ApplicationDbContext. The collection of the NetworkEvents exists to mock a queue where we will receive the real NetworkEvents.
You will need to implement a component whose task is to process those Network Events and store them in a data structure (you must define it in the Usage class) to help you achieve the reports defined above.
You are free to use any kind of component to process those NEs. Explain your choices in (1)
UI
Create a basic UI that presents the calculation results. 
The client SPA, is in the solution folder. You can run it by following the steps:
1. Open the terminal and navigate the SPA folder
2. For the first time you run, use the "npm install" command.
3. Run "ng serve" command.
 
Use the "summary" component to display the required data fetching from your server application, following the attached image.
Also, provide a refresh functionality.
 

 
Bonus – Display a report in the "usage-data" page component.
 
 
GOOD LUCK
 
