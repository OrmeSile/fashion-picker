BEGIN;
    insert into "outfit-picker".public."Season" values
    (gen_random_uuid(), 'Spring'),
    (gen_random_uuid(), 'Summer'),
    (gen_random_uuid(), 'Autumn'),
    (gen_random_uuid(), 'Winter');
COMMIT;