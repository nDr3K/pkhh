-- USERS
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL
);

-- GAMES
CREATE TABLE games (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    generation INT NOT NULL,
    official BOOLEAN DEFAULT TRUE,
    region TEXT
);

-- TYPES
CREATE TABLE types (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

-- ABILITIES
CREATE TABLE abilities (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

-- STATS
CREATE TABLE stats (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

-- NATURES
CREATE TABLE natures (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    increased_stat_id INT REFERENCES stats(id),
    decreased_stat_id INT REFERENCES stats(id)
);

-- ITEMS
CREATE TABLE items (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

-- CATEGORIES (Physical, Special, Status)
CREATE TABLE categories (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

-- MOVES
CREATE TABLE moves (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    type_id INT REFERENCES types(id),
    category_id INT REFERENCES categories(id),
    power INT,
    accuracy INT,
    pp INT,
    effect TEXT,
    priority INT DEFAULT 0
);

-- MOVE LEARNING METHOD
CREATE TABLE movelearning_methods (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

-- MOVE LEARNING (Per game)
CREATE TABLE movelearning (
    id SERIAL PRIMARY KEY,
    pokemon_id INT,
    move_id INT REFERENCES moves(id),
    method_id INT REFERENCES movelearning_methods(id),
    game_id INT REFERENCES games(id),
    level INT,
    tm_number TEXT,
    is_tutor BOOLEAN DEFAULT FALSE,
    is_egg_move BOOLEAN DEFAULT FALSE
);

-- POKEMON (Species)
CREATE TABLE pokemon (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    dex_number INT NOT NULL,
    game_id INT REFERENCES games(id),
    hp INT,
    att INT,
    def INT,
    spa INT,
    spd INT,
    spe INT,
    type1_id INT REFERENCES types(id),
    type2_id INT REFERENCES types(id),
    form_name TEXT,
    is_regional_form BOOLEAN DEFAULT FALSE,
    is_mega BOOLEAN DEFAULT FALSE,
    is_gigantamax BOOLEAN DEFAULT FALSE
);

-- FORMS (More specific)
CREATE TABLE forms (
    id SERIAL PRIMARY KEY,
    pokemon_id INT REFERENCES pokemon(id),
    name TEXT NOT NULL,
    is_regional BOOLEAN DEFAULT FALSE,
    is_mega BOOLEAN DEFAULT FALSE,
    is_gigantamax BOOLEAN DEFAULT FALSE,
    form_order INT,
    type1_id INT REFERENCES types(id),
    type2_id INT REFERENCES types(id)
);

-- POKEMON ABILITIES (mapping species to abilities)
CREATE TABLE pokemon_abilities (
    id SERIAL PRIMARY KEY,
    pokemon_id INT REFERENCES pokemon(id),
    ability_id INT REFERENCES abilities(id),
    is_hidden BOOLEAN DEFAULT FALSE,
    ability_slot INT
);

-- POKEMON INSTANCE (Specific Pokémon, e.g. from save file)
CREATE TABLE pokemon_instances (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id),
    game_id INT REFERENCES games(id),
    pokemon_id INT REFERENCES pokemon(id),
    form_id INT REFERENCES forms(id),
    nickname TEXT,
    gender TEXT CHECK (gender IN ('Male', 'Female', 'Genderless')),
    level INT,
    shiny BOOLEAN DEFAULT FALSE,
    nature_id INT REFERENCES natures(id),
    held_item_id INT REFERENCES items(id),
    ability_id INT REFERENCES abilities(id),
    move1_id INT REFERENCES moves(id),
    move2_id INT REFERENCES moves(id),
    move3_id INT REFERENCES moves(id),
    move4_id INT REFERENCES moves(id),
    iv_hp INT,
    iv_att INT,
    iv_def INT,
    iv_spa INT,
    iv_spd INT,
    iv_spe INT,
    ev_hp INT,
    ev_att INT,
    ev_def INT,
    ev_spa INT,
    ev_spd INT,
    ev_spe INT,
    original_trainer_name TEXT,
    original_trainer_id TEXT,
    met_level INT,
    met_location TEXT,
    ribbons TEXT -- Optional, could be expanded to a relation
);

-- TEAMS
CREATE TABLE teams (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id),
    game_id INT REFERENCES games(id),
    name TEXT NOT NULL,
    description TEXT,
    created_at TIMESTAMP DEFAULT NOW()
);

-- TEAM MEMBERS
CREATE TABLE team_members (
    id SERIAL PRIMARY KEY,
    team_id INT REFERENCES teams(id),
    pokemon_instance_id INT REFERENCES pokemon_instances(id)
);

-- BOXES
CREATE TABLE boxes (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id),
    game_id INT REFERENCES games(id),
    name TEXT
);

-- BOX SLOTS (Each Pokémon in a box)
CREATE TABLE box_slots (
    id SERIAL PRIMARY KEY,
    box_id INT REFERENCES boxes(id),
    pokemon_instance_id INT REFERENCES pokemon_instances(id),
    slot_number INT
);