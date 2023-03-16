CREATE TABLE users (
  user_id SERIAL PRIMARY KEY,
  user_name VARCHAR(50) NOT NULL,
  email VARCHAR(100) NOT NULL,
  password VARCHAR(255) NOT NULL
);

CREATE TABLE projects (
  project_id SERIAL PRIMARY KEY,
  project_name VARCHAR(50) NOT NULL,
  description TEXT
);

CREATE TABLE tasks (
  task_id SERIAL PRIMARY KEY,
  task_name VARCHAR(50) NOT NULL,
  description TEXT,
  project_id INTEGER NOT NULL,
  FOREIGN KEY (project_id) REFERENCES projects(project_id)
);

CREATE TABLE resources (
  resource_id SERIAL PRIMARY KEY,
  resource_name VARCHAR(50) NOT NULL,
  resource_available BOOLEAN DEFAULT true
);

CREATE TABLE companies (
  company_id SERIAL PRIMARY KEY,
  company_name VARCHAR(50) NOT NULL
);

CREATE TABLE admins (
  admin_id SERIAL PRIMARY KEY,
  admin_name VARCHAR(50) NOT NULL
);

CREATE TABLE user_works_for_company (
  user_id INTEGER NOT NULL,
  company_id INTEGER NOT NULL,
  PRIMARY KEY (user_id, company_id),
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (company_id) REFERENCES companies(company_id)
);

CREATE TABLE user_scheduled_for_project (
  user_id INTEGER NOT NULL,
  project_id INTEGER NOT NULL,
  PRIMARY KEY (user_id, project_id),
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (project_id) REFERENCES projects(project_id)
);

CREATE TABLE user_scheduled_for_resources (
  user_id INTEGER NOT NULL,
  resource_id INTEGER NOT NULL,
  PRIMARY KEY (user_id, resource_id),
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (resource_id) REFERENCES resources(resource_id)
);

CREATE TABLE user_tasks (
  user_task_id SERIAL PRIMARY KEY,
  user_id INTEGER NOT NULL,
  task_id INTEGER NOT NULL,
  resource_id INTEGER,
  check_in_time TIMESTAMP,
  check_out_time TIMESTAMP,
  task_completed BOOLEAN DEFAULT false,
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (task_id) REFERENCES tasks(task_id),
  FOREIGN KEY (resource_id) REFERENCES resources(resource_id)
);


-- 1. user Logins
SELECT *
FROM users
WHERE users.email = 'RAHUL@GMAIL.COM' AND password = 'HASH_PASSWORD';

-- 2. user chooses a company
SELECT company_id, company_name
FROM companies
WHERE company_id IN (
    SELECT company_id
    FROM user_works_for_company
    WHERE user_id = 1234
);
-- let the company_id is company_123

-- we pick the database url to which the subsequent queries should hit, lets say we maintain a cache
-- server with redis having (key, value) = (company_id, database_server_url)
-- let say it looks like following,
-- company_database_map = {
--       "ditioas"     : "https://ditioas.mysql.database.azure.com",
--       "company_123" : "https://company_123.mysql.database.azure.com",
--       "vassabank"   : "https://vassabank.mysql.database.azure.com",
-- }
-- so the following queries will go to "https://company_123.mysql.database.azure.com".

-- 3. user gets a list of project which are scheduled for them and selects one project.
SELECT project_id, project_name, description 
FROM projects 
WHERE project_id IN (
    SELECT project_id 
    FROM user_scheduled_for_project 
    WHERE user_id = 1234
);
-- let user chooses a project with project_id = 1000


-- 4. user gets a list of resources which are scheduled for them and selects one resource.
SELECT resource_id, resource_name
FROM resources
WHERE resource_available = true
AND resource_id IN (
    SELECT resource_id
    FROM user_scheduled_for_resources
    WHERE user_id = 1234
);
-- let user chooses a resource with resource_id = 2000

-- 5. user get a list of tasks belonging to the chosen project and chooses one task.
SELECT task_id, task_name, description 
FROM tasks 
WHERE project_id = 1000;
--let user chooses a task with task_id = 3000

-- 6. user clicks on check-in and records the check-in time and makes the resource unavailable.
INSERT INTO user_tasks (user_id, task_id, resource_id, check_in_time)
VALUES (1234, 3000, 2000, '2023-01-25 09:00:00');
--let user_task_id = 4000

UPDATE resources
SET resource_available = false
WHERE resource_id = 2000;

-- 7. user clicks on check-out and records the check-out time and makes the resource available.
SELECT user_task_id
FROM user_tasks
WHERE user_id = 1234 AND task_id = 3000 AND resource_id = 2000 AND task_completed = false;

UPDATE user_tasks
SET check_out_time = '2023-01-25 12:00:00', task_completed = true
WHERE user_task_id = 4000;

UPDATE resources
SET resource_available = true
WHERE resource_id = 2000;
