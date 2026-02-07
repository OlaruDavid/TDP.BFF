-- EXTENSII
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

DELETE FROM users WHERE email = 'olarudavid@yahoo.com'

-- USERS
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    email TEXT NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    first_name TEXT,
    last_name TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
select * from users

-- PROJECTS
CREATE TABLE projects (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name TEXT NOT NULL,
    slug TEXT NOT NULL,
    owner_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    UNIQUE (owner_id, slug)
);
select * from projects

-- PROJECT MEMBERS
CREATE TABLE project_members (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    project_id UUID NOT NULL REFERENCES projects(id) ON DELETE CASCADE,
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    role TEXT NOT NULL CHECK (role IN ('owner', 'member')),
    joined_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    UNIQUE (project_id, user_id)
);
SELECT * FROM project_members

-- TASKS (capitole mari)
CREATE TABLE tasks (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    project_id UUID NOT NULL REFERENCES projects(id) ON DELETE CASCADE,
    name TEXT NOT NULL,
    color TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_by UUID REFERENCES users(id),
    updated_at TIMESTAMPTZ,
    updated_by UUID REFERENCES users(id)
);
SELECT * FROM tasks

-- SUBTASKS (ce apare pe Board)
CREATE TABLE subtasks (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    task_id UUID NOT NULL REFERENCES tasks(id) ON DELETE CASCADE,
    title TEXT NOT NULL,
    description TEXT,
    column_name TEXT NOT NULL CHECK (column_name IN ('todo', 'progress', 'done')),
    position INTEGER NOT NULL,
    assignee_id UUID REFERENCES users(id),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_by UUID REFERENCES users(id),
    updated_at TIMESTAMPTZ,
    updated_by UUID REFERENCES users(id)
);
SELECT * FROM subtasks

-- ISSUES
CREATE TABLE issues (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    project_id UUID NOT NULL REFERENCES projects(id) ON DELETE CASCADE,
    task_id UUID REFERENCES tasks(id) ON DELETE SET NULL,
    subtask_id UUID REFERENCES subtasks(id) ON DELETE SET NULL,
    title TEXT NOT NULL,
    description TEXT,
    created_by UUID NOT NULL REFERENCES users(id),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
ALTER TABLE tasks
DROP CONSTRAINT tasks_created_by_fkey;

ALTER TABLE tasks
ADD CONSTRAINT tasks_created_by_fkey
FOREIGN KEY (created_by)
REFERENCES users(id)
ON DELETE SET NULL;
ALTER TABLE subtasks
DROP CONSTRAINT subtasks_created_by_fkey;

ALTER TABLE subtasks
ADD CONSTRAINT subtasks_created_by_fkey
FOREIGN KEY (created_by)
REFERENCES users(id)
ON DELETE SET NULL;

ALTER TABLE issues
DROP CONSTRAINT issues_created_by_fkey;

ALTER TABLE issues
ADD CONSTRAINT issues_created_by_fkey
FOREIGN KEY (created_by)
REFERENCES users(id)
ON DELETE SET NULL;

ALTER TABLE issues
ALTER COLUMN created_by DROP NOT NULL;


