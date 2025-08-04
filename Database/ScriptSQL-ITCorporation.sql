-- DROP TABLE IF EXISTS "Employee_Projects";
-- DROP TABLE IF EXISTS "EmployeeDetails";
-- DROP TABLE IF EXISTS "Employees";
-- DROP TABLE IF EXISTS "Projects";

-----------------------------------------------------------------------------------------------------------------------------------------------
-- DELETE DATA FROM TABLE

DELETE FROM "Employee_Projects";
DELETE FROM "EmployeeDetails";
DELETE FROM "Employees";
DELETE FROM "Projects";


-----------------------------------------------------------------------------------------------------------------------------------------------
-- RESET IDENTITY IN TABLES

TRUNCATE TABLE
    "Employee_Projects",
    "EmployeeDetails",
    "Employees",
    "Projects"
RESTART IDENTITY  -- скидає всі sequence
CASCADE;          -- якщо є зовнішні ключі


-----------------------------------------------------------------------------------------------------------------------------------------------
-- INSERT INTO  (Seeding database)

--//Layout - INSERT INTO//              					--
--								   							--
-- INSERT INTO "Table_name" ("Field_1", "Field_2") VALUES   --
-- ("ABC", 123),											--
-- ("xyz", 098);              								--

INSERT INTO "Projects" ("Name", "Status") VALUES
('Travel App', 'Completed'),
('E-Commerce Platform', 'InProgress'),
('Booking System', 'Completed'),
('Inventory Tracker', 'InProgress'),
('CRM Dashboard', 'ToDo');

INSERT INTO "Employees" ("Id", "FirstName", "LastName") VALUES
(1, 'John', 'Doe'),
(2, 'Alice', 'Smith'),
(3, 'Bob', 'Johnson'),
(4, 'Emily', 'Davis'),
(5, 'Michael', 'Brown'),
(7, 'Patryk', 'Samek');

INSERT INTO "EmployeeDetails" ("Email", "Role", "EmployeeId") VALUES
('john.doe@example.com', 'Developer', 1),
('alice.smith@example.com', 'Team Lead', 2),
('bob.johnson@example.com', 'QA', 3),
('emily.davis@example.com', 'Designer', 4),
('michael.brown@example.com', 'Developer', 5);

INSERT INTO "Employee_Projects" ("EmployeeId", "ProjectId") VALUES
(1, 1), (2, 1), (5, 1), 
(1, 2), (2, 2), (3, 2), (4, 2), (5, 2),
(2, 3), (3, 3), (4, 3), 
(1, 4), (3, 4), (2, 4), 
(2, 5), (1, 5), (5, 5), (4, 5);

-----------------------------------------------------------------------------------------------------------------------------------------------
-- SELECT

--//Layout - SELECT//              --
--								   --
-- SELECT * 					   --
-- FROM "Table_name";              --

SELECT * FROM "EmployeeDetails";
SELECT * FROM "Employees";
SELECT * FROM "Projects";
SELECT * FROM "Employee_Projects";


--//Layout - SELECT//              --
--								   --
-- SELECT                          --
-- 		"Column_name"              --
-- FROM "Table_name";              --

-- Display the "Projects" table
SELECT "Name"
FROM "Projects";

-- Display the "Projects" table with the columns "Id" and "Name"
SELECT 
"Id", 
"Name"
FROM "Projects";

-----------------------------------------------------------------------------------------------------------------------------------------------
-- WHERE

--//Layout - WHERE//                            --
--								                --
-- SELECT * FROM "Table_name"                   --
-- WHERE "Table_name"."Column_name" = '...'     --

-- Find a project by ID
SELECT * FROM "Projects"
WHERE "Projects"."Id" = 2;

-- Find a project by its name
SELECT 
p."Id", 
p."Name" 
FROM "Projects" AS p
WHERE p."Name" = 'Travel App';

-- Display the projects whose IDs are between 3 and 5
SELECT * FROM "Projects" p 
WHERE p."Id" BETWEEN 3 AND 5

-- Show the roles whose value is QA or Developer
SELECT * FROM "EmployeeDetails" ed 
WHERE ed."Role" = 'QA' OR ed."Role" = 'Developer'


-----------------------------------------------------------------------------------------------------------------------------------------------
-- ORDER BY

--//Layout - ORDER BY//              					--
--								   					--
-- SELECT * 					   					--
-- FROM "Table_name"               					--
-- OREDR BY "Table_name"."Column_name" ASC(DESC) 	--

-- Display all project names in ascending alphabetical order
SELECT "Name"
FROM "Projects" AS p
ORDER BY p."Name" ASC 

-- Display every column for all projects, sorted by project name in descending (Z–A) order
SELECT *
FROM "Projects" AS p
ORDER BY p."Name" DESC 


-----------------------------------------------------------------------------------------------------------------------------------------------
-- GROUP BY

--//Layout - GROUP BY//              		--
--								   			--
-- SELECT                                   --
--		"Column_name",						--
--		COUNT(*) AS "Num_order"			    --
-- FROM "Table_name"               			--
-- GROUP BY "Table_name"."Column_name"      --

-- How many completed/incomplete projects by status
SELECT 
p."Status",
COUNT(*) AS "ProjectsCount"
FROM "Projects" p
GROUP BY p."Status";

-- Number of projects per employee
SELECT 
e."FirstName" || ' ' || e."LastName" AS "Employee",
COUNT(ep."ProjectId") AS "ProjectCount"
FROM "Employees" e
LEFT JOIN "Employee_Projects" ep ON e."Id" = ep."EmployeeId"
GROUP BY e."Id"
ORDER BY "ProjectCount" DESC;

-----------------------------------------------------------------------------------------------------------------------------------------------
-- JOIN (LEFT, RIGHT, FULL)

--//Layout - JOINs//              								--
--								   								--
-- SELECT  *                                					--
-- FROM "Table_name_A" tA       								--
-- LEFT JOIN "Table_name_B" tB ON tA."Id_tA" = "Id_tB"			--


SELECT *
FROM   "Employees" e
LEFT JOIN "EmployeeDetails" d ON e."Id" = d."EmployeeId"

SELECT *
FROM   "Employees" e
RIGHT JOIN "EmployeeDetails" d ON e."Id" = d."EmployeeId"

SELECT *
FROM   "Employees" e
FULL JOIN "EmployeeDetails" d ON d."EmployeeId" = e."Id" 


-- Display all projects and their members using (LEFT JOIN)
SELECT * FROM "Projects" p
LEFT JOIN "Employee_Projects" ep ON p."Id" = ep."ProjectId"
LEFT JOIN "Employees" e ON e."Id" = ep."EmployeeId"
ORDER BY p."Id", e."Id" ASC;

-- Display all projects and their members with detailed information using (LEFT JOIN)
SELECT * FROM "Projects" p
LEFT JOIN "Employee_Projects" ep ON p."Id" = ep."ProjectId"
LEFT JOIN "Employees" e ON e."Id" = ep."EmployeeId"
LEFT JOIN "EmployeeDetails" ed ON e."Id" = ed."EmployeeId"
ORDER BY p."Id", e."Id" ASC;

-- Display projects without employees, sorted by project ID in descending order using (LEFT JOIN)
SELECT * FROM "Projects" p
LEFT JOIN "Employee_Projects" ep ON p."Id" = ep."ProjectId"
LEFT JOIN "Employees" e ON e."Id" = ep."EmployeeId"
LEFT JOIN "EmployeeDetails" ed ON e."Id" = ed."EmployeeId"
WHERE e."LastName" IS NULL
ORDER BY p."Id" DESC;

-- Display employees who have the role "Team Lead" using (LEFT JOIN)
SELECT * FROM "Employees" e
LEFT JOIN "EmployeeDetails" ed ON e."Id" = ed."EmployeeId"
WHERE ed."Role" LIKE 'Team%';

-- Display all QA employees using (RIGHT JOIN)
SELECT * FROM "EmployeeDetails" ed
RIGHT JOIN "Employees" e ON e."Id" = ed."EmployeeId"
WHERE ed."Role" LIKE '_A';

-- Display all projects and their existing members using (FULL JOIN)
SELECT * FROM "Projects" p
FULL JOIN "Employee_Projects" ep ON p."Id" = ep."ProjectId"
FULL JOIN "Employees" e ON e."Id" = ep."EmployeeId"
WHERE e."Id" IS NOT NULL
ORDER BY p."Id", e."Id";

-----------------------------------------------------------------------------------------------------------------------------------------------
-- STORED PROCEDURES (PROCEDURE)

--//Layout - Stored Procedure//                   --
--          CREATE PROCEDURE [proc_name]          --
--          (                                     --
--              [param1] [datatype],              --
--              [param2] [datatype]               --
--          )                                     --
--          LANGUAGE plpgsql                      --
--          AS $$                                 --
--          BEGIN                                 --
--              -- logic here                     --
--          END;                                  --
--          $$                                    --


-- Update project by id
CREATE OR REPLACE PROCEDURE SP_Update_Project(
    p_id INT,
    p_name TEXT,
    p_status TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE "Projects"
    SET "Name" = p_name,
        "Status" = p_status
    WHERE "Id" = p_id;
END;
$$;

-- DROP PROCEDURE SP_Update_Project(INT, TEXT, TEXT);

CALL SP_Update_Project(6, 'Updated Project', 'InProgress');

SELECT * FROM "Projects" p WHERE p."Id" = 6;



-- Create new Employee
CREATE PROCEDURE P_New_Project(
	p_name text,
	p_status text
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO "Projects"("Name", "Status")
    VALUES (p_name, p_status);
END;
$$;

--DROP PROCEDURE IF EXISTS P_New_Project(text, text);

CALL P_New_Project('New AI Platform', 'InProgress');

SELECT * FROM "Projects" p WHERE p."Name" LIKE 'New AI Platform'
-----------------------------------------------------------------------------------------------------------------------------------------------
-- CTE

--//Layout - Common Table Expression//--

-- 			WITH [CTE_NAME] AS		--
--			(						--
--				<inner_query>		--
--			)						--
--				<outer_query>		--


-- Returns the number of projects for each specific status
WITH CTE_ProjectStatus AS (
	SELECT 
	p."Status"
	FROM "Projects" p 
)
SELECT
"Status",
count(*) AS "Count Project"
FROM CTE_ProjectStatus
GROUP BY "Status"


-- Return the number of employees who hold a given role
WITH CTE_ProjectRoles AS (
    SELECT
    p."Name" AS ProjectName,
    ed."Role"
    FROM "Projects" p
    JOIN "Employee_Projects" ep ON p."Id" = ep."ProjectId"
    JOIN "Employees" e ON ep."EmployeeId" = e."Id"
    JOIN "EmployeeDetails" ed ON e."Id" = ed."EmployeeId"
)
SELECT
ProjectName,
"Role",
COUNT(*) AS "Role Count"
FROM CTE_ProjectRoles
GROUP BY ProjectName, "Role"
ORDER BY ProjectName, "Role";