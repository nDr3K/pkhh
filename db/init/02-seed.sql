-- CATEGORY
INSERT INTO "Categories" ("Id", "Name") VALUES
(1, 'Physical'),
(2, 'Special'),
(3, 'Status');

-- TYPE
INSERT INTO "Types" ("Id", "Name") VALUES
(1, 'Normal'),
(2, 'Fighting'),
(3, 'Flying'),
(4, 'Poison'),
(5, 'Ground'),
(6, 'Rock'),
(7, 'Bug'),
(8, 'Ghost'),
(9, 'Steel'),
(10, 'Fire'),
(11, 'Water'),
(12, 'Grass'),
(13, 'Electric'),
(14, 'Psychic'),
(15, 'Ice'),
(16, 'Dragon'),
(17, 'Dark'),
(18, 'Fairy'),
(19, 'Stellar'),
(20, 'Bird');
(21, '???');

-- STAT
INSERT INTO "Stats" ("Id", "Name") VALUES
(1, 'Attack'),
(2, 'Defense'),
(3, 'Special Attack'),
(4, 'Special Defense'),
(5, 'Special'),
(6, 'Speed'),
(7, 'HP');

-- NATURE
INSERT INTO "Natures" ("Id", "Name", "IncreasedStatId", "DecreasedStatId") VALUES
(1, 'Hardy', NULL, NULL),
(2, 'Lonely', 1, 2),
(3, 'Brave', 1, 6),
(4, 'Adamant', 1, 3),
(5, 'Naughty', 1, 4),
(6, 'Bold', 2, 1),
(7, 'Docile', NULL, NULL),
(8, 'Relaxed', 2, 6),
(9, 'Impish', 2, 3),
(10, 'Lax', 2, 4),
(11, 'Timid', 6, 1),
(12, 'Hasty', 6, 2),
(13, 'Serious', NULL, NULL),
(14, 'Jolly', 6, 3),
(15, 'Naive', 6, 4),
(16, 'Modest', 3, 1),
(17, 'Mild', 3, 2),
(18, 'Quiet', 3, 6),
(19, 'Bashful', NULL, NULL),
(20, 'Rash', 3, 4),
(21, 'Calm', 4, 1),
(22, 'Gentle', 4, 2),
(23, 'Sassy', 4, 6),
(24, 'Careful', 4, 3),
(25, 'Quirky', NULL, NULL);
