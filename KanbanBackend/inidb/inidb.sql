PGDMP                         {            Kanban    15.2    15.2 $               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16398    Kanban    DATABASE     |   CREATE DATABASE "Kanban" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "Kanban";
                postgres    false            �            1259    16439    board_columns    TABLE     �   CREATE TABLE public.board_columns (
    column_id integer NOT NULL,
    column_title character varying(30) NOT NULL,
    card_id integer,
    board_id integer NOT NULL
);
 !   DROP TABLE public.board_columns;
       public         heap    postgres    false            �            1259    16438    board_columns_column_id_seq    SEQUENCE     �   CREATE SEQUENCE public.board_columns_column_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public.board_columns_column_id_seq;
       public          postgres    false    221                        0    0    board_columns_column_id_seq    SEQUENCE OWNED BY     [   ALTER SEQUENCE public.board_columns_column_id_seq OWNED BY public.board_columns.column_id;
          public          postgres    false    220            �            1259    16427    boards    TABLE     �   CREATE TABLE public.boards (
    board_id integer NOT NULL,
    board_name character varying(30) NOT NULL,
    column_id integer,
    user_id integer NOT NULL
);
    DROP TABLE public.boards;
       public         heap    postgres    false            �            1259    16430    boards_board_id_seq    SEQUENCE     �   CREATE SEQUENCE public.boards_board_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.boards_board_id_seq;
       public          postgres    false    218            !           0    0    boards_board_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.boards_board_id_seq OWNED BY public.boards.board_id;
          public          postgres    false    219            �            1259    16407    cards    TABLE     �   CREATE TABLE public.cards (
    card_id integer NOT NULL,
    card_name character varying(50) NOT NULL,
    card_text text,
    card_date date NOT NULL,
    column_id integer NOT NULL
);
    DROP TABLE public.cards;
       public         heap    postgres    false            �            1259    16406    cards_card_id_seq    SEQUENCE     �   CREATE SEQUENCE public.cards_card_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.cards_card_id_seq;
       public          postgres    false    217            "           0    0    cards_card_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.cards_card_id_seq OWNED BY public.cards.card_id;
          public          postgres    false    216            �            1259    16400    users    TABLE     �   CREATE TABLE public.users (
    user_id integer NOT NULL,
    user_email character varying(50) NOT NULL,
    user_name character varying(50) NOT NULL,
    user_password character varying(50) NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false            �            1259    16399    users_user_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.users_user_id_seq;
       public          postgres    false    215            #           0    0    users_user_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;
          public          postgres    false    214            w           2604    16442    board_columns column_id    DEFAULT     �   ALTER TABLE ONLY public.board_columns ALTER COLUMN column_id SET DEFAULT nextval('public.board_columns_column_id_seq'::regclass);
 F   ALTER TABLE public.board_columns ALTER COLUMN column_id DROP DEFAULT;
       public          postgres    false    220    221    221            v           2604    16431    boards board_id    DEFAULT     r   ALTER TABLE ONLY public.boards ALTER COLUMN board_id SET DEFAULT nextval('public.boards_board_id_seq'::regclass);
 >   ALTER TABLE public.boards ALTER COLUMN board_id DROP DEFAULT;
       public          postgres    false    219    218            u           2604    16410    cards card_id    DEFAULT     n   ALTER TABLE ONLY public.cards ALTER COLUMN card_id SET DEFAULT nextval('public.cards_card_id_seq'::regclass);
 <   ALTER TABLE public.cards ALTER COLUMN card_id DROP DEFAULT;
       public          postgres    false    216    217    217            t           2604    16403    users user_id    DEFAULT     n   ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);
 <   ALTER TABLE public.users ALTER COLUMN user_id DROP DEFAULT;
       public          postgres    false    215    214    215                      0    16439    board_columns 
   TABLE DATA           S   COPY public.board_columns (column_id, column_title, card_id, board_id) FROM stdin;
    public          postgres    false    221   �(                 0    16427    boards 
   TABLE DATA           J   COPY public.boards (board_id, board_name, column_id, user_id) FROM stdin;
    public          postgres    false    218   �(                 0    16407    cards 
   TABLE DATA           T   COPY public.cards (card_id, card_name, card_text, card_date, column_id) FROM stdin;
    public          postgres    false    217   )                 0    16400    users 
   TABLE DATA           N   COPY public.users (user_id, user_email, user_name, user_password) FROM stdin;
    public          postgres    false    215   P)       $           0    0    board_columns_column_id_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public.board_columns_column_id_seq', 3, true);
          public          postgres    false    220            %           0    0    boards_board_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.boards_board_id_seq', 7, true);
          public          postgres    false    219            &           0    0    cards_card_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.cards_card_id_seq', 3, true);
          public          postgres    false    216            '           0    0    users_user_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.users_user_id_seq', 19, true);
          public          postgres    false    214                       2606    16444     board_columns board_columns_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public.board_columns
    ADD CONSTRAINT board_columns_pkey PRIMARY KEY (column_id);
 J   ALTER TABLE ONLY public.board_columns DROP CONSTRAINT board_columns_pkey;
       public            postgres    false    221            }           2606    16433    boards boards_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.boards
    ADD CONSTRAINT boards_pkey PRIMARY KEY (board_id);
 <   ALTER TABLE ONLY public.boards DROP CONSTRAINT boards_pkey;
       public            postgres    false    218            {           2606    16414    cards cards_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.cards
    ADD CONSTRAINT cards_pkey PRIMARY KEY (card_id);
 :   ALTER TABLE ONLY public.cards DROP CONSTRAINT cards_pkey;
       public            postgres    false    217            y           2606    16405    users users_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    215            �           2606    16450 )   board_columns board_columns_board_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.board_columns
    ADD CONSTRAINT board_columns_board_id_fkey FOREIGN KEY (board_id) REFERENCES public.boards(board_id);
 S   ALTER TABLE ONLY public.board_columns DROP CONSTRAINT board_columns_board_id_fkey;
       public          postgres    false    218    221    3197            �           2606    16445 (   board_columns board_columns_card_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.board_columns
    ADD CONSTRAINT board_columns_card_id_fkey FOREIGN KEY (card_id) REFERENCES public.cards(card_id);
 R   ALTER TABLE ONLY public.board_columns DROP CONSTRAINT board_columns_card_id_fkey;
       public          postgres    false    221    3195    217            �           2606    16455    boards boards_column_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.boards
    ADD CONSTRAINT boards_column_id_fkey FOREIGN KEY (column_id) REFERENCES public.board_columns(column_id);
 F   ALTER TABLE ONLY public.boards DROP CONSTRAINT boards_column_id_fkey;
       public          postgres    false    218    3199    221            �           2606    16460    boards boards_user_id_fkey    FK CONSTRAINT     ~   ALTER TABLE ONLY public.boards
    ADD CONSTRAINT boards_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id);
 D   ALTER TABLE ONLY public.boards DROP CONSTRAINT boards_user_id_fkey;
       public          postgres    false    3193    218    215                  x�3�L����4�2��R!�=... T&�         <   x�3��N�KJ����4��2����\K.���rM9�S�J*�\sμ�r(;F��� �&         $   x�3�,N��b�D�4202�50"N#�=... ݪ	�         �   x�3��,.)��Kw�M���+*�r9+�"�L����3}´}#*�-,C|C+\�ÊB�}]��*"<B�m�-����$���qz%���{:������W���y�{��T���9���F�r��qqq ��.?     