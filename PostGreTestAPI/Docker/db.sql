-- db.sql
CREATE SCHEMA test_project

CREATE TABLE test_project.Potato (
	Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	Name VARCHAR(100) NOT NULL
);
