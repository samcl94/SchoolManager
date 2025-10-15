-- Create a dedicated user for the app
CREATE USER school_user WITH PASSWORD 'Pkostu22z1';

-- Grant privileges to the user
GRANT CONNECT ON DATABASE school TO school_user;

GRANT USAGE ON SCHEMA public TO school_user;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO school_user;

-- Important for future tables: give privileges automatically
ALTER DEFAULT PRIVILEGES IN SCHEMA public
   GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO school_user;