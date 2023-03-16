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
  resource_name VARCHAR(50) NOT NULL
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

CREATE TABLE user_scheduled_for_task (
  user_id INTEGER NOT NULL,
  task_id INTEGER NOT NULL,
  PRIMARY KEY (user_id, task_id),
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (task_id) REFERENCES tasks(task_id)
);

CREATE TABLE user_tasks (
  user_task_id SERIAL PRIMARY KEY,
  user_id INTEGER NOT NULL,
  task_id INTEGER NOT NULL,
  resource_id INTEGER,
  check_in_time TIMESTAMP,
  check_out_time TIMESTAMP,
  FOREIGN KEY (user_id) REFERENCES users(user_id),
  FOREIGN KEY (task_id) REFERENCES tasks(task_id),
  FOREIGN KEY (resource_id) REFERENCES resources(resource_id)
);




