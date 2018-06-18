CREATE EXTENSION pgcrypto;
CREATE TABLE contacts(
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  name TEXT,
  email TEXT
);