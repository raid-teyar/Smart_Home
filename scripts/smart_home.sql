create table room
(
    id   int auto_increment
        primary key,
    name varchar(50) not null,
    constraint table_name_id_uindex
        unique (id)
);

create table device
(
    id    int auto_increment
        primary key,
    name  varchar(100) not null,
    type  varchar(50)  not null,
    state tinyint(1)   not null,
    value varchar(50)  not null,
    room  int          not null,
    constraint device_id_uindex
        unique (id),
    constraint device_room_id_fk
        foreign key (room) references room (id)
);

create table user
(
    id       int auto_increment
        primary key,
    email    varchar(100) not null,
    fullname varchar(100) not null,
    password varchar(50)  not null,
    usertype varchar(50)  not null,
    constraint table_name_id_uindex
        unique (id)
);

create table history
(
    id     int auto_increment
        primary key,
    time   datetime     not null,
    action varchar(100) not null,
    userid int          not null,
    constraint history_id_uindex
        unique (id),
    constraint history_user_id_fk
        foreign key (userid) references user (id)
);

create table variables
(
    name  varchar(50) not null
        primary key,
    value tinyint(1)  null,
    constraint variables_name_uindex
        unique (name)
);

