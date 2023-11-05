CREATE TABLE adresa (
    id_adres      NUMBER NOT NULL,
    ulice         VARCHAR2(32) NOT NULL,
    mesto         VARCHAR2(32) NOT NULL,
    cislo_popisne VARCHAR2(10) NOT NULL,
    psc           CHAR(10) NOT NULL,
    stat          VARCHAR2(32) NOT NULL
);

ALTER TABLE adresa ADD CONSTRAINT adresa_pk PRIMARY KEY ( id_adres );

CREATE TABLE bank (
    id_bank NUMBER NOT NULL,
    kod     NUMBER NOT NULL,
    nazev   VARCHAR2(32) NOT NULL
);

ALTER TABLE bank ADD CONSTRAINT bank_pk PRIMARY KEY ( id_bank );

CREATE TABLE bankomat (
    id_bankomat NUMBER NOT NULL,
    nazev       VARCHAR2(32) NOT NULL,
    id_adres    NUMBER NOT NULL,
    id_bank     NUMBER NOT NULL,
    id_status   NUMBER NOT NULL
);

ALTER TABLE bankomat ADD CONSTRAINT bankomat_pk PRIMARY KEY ( id_bankomat );

CREATE TABLE debetni_karta (
    id_karta NUMBER NOT NULL,
    limit    NUMBER NOT NULL
);

ALTER TABLE debetni_karta ADD CONSTRAINT debetni_karta_pk PRIMARY KEY ( id_karta );

CREATE TABLE karta (
    id_karta        NUMBER NOT NULL,
    cislo_karty     NUMBER NOT NULL,
    platebni_system VARCHAR2(32) NOT NULL,
    platnost        DATE NOT NULL,
    typ             VARCHAR2(1) NOT NULL,
    id_ucet         NUMBER NOT NULL,
    jmeno           VARCHAR2(32) NOT NULL,
    prijmeni        VARCHAR2(32) NOT NULL
);

ALTER TABLE karta ADD CONSTRAINT karta_pk PRIMARY KEY ( id_karta );

CREATE TABLE klient (
    id_klient      NUMBER NOT NULL,
    cislo_prukazu  NUMBER NOT NULL,
    jmeno          VARCHAR2(32) NOT NULL,
    prijmeni       VARCHAR2(32) NOT NULL,
    telefoni_cislo VARCHAR2(20) NOT NULL,
    id_adres       NUMBER NOT NULL,
    id_bank        NUMBER NOT NULL,
    id_zamestnanec NUMBER NOT NULL
);

ALTER TABLE klient ADD CONSTRAINT klient_pk PRIMARY KEY ( id_klient );

CREATE TABLE klient_schuzka (
    id_klient  NUMBER NOT NULL,
    id_schuzka NUMBER NOT NULL
);

ALTER TABLE klient_schuzka ADD CONSTRAINT klient_schuzka_pk PRIMARY KEY ( id_klient,
                                                                          id_schuzka );

CREATE TABLE kreditni_karta (
    id_karta            NUMBER NOT NULL,
    uverzovy_limit      NUMBER NOT NULL,
    bezurocne_obdobi    NUMBER NOT NULL,
    urokova_sazba       NUMBER NOT NULL,
    povina_platba_uveru NUMBER NOT NULL
);

ALTER TABLE kreditni_karta ADD CONSTRAINT kreditni_karta_pk PRIMARY KEY ( id_karta );

CREATE TABLE operace (
    id_operace     NUMBER NOT NULL,
    castka         NUMBER NOT NULL,
    datum_zacatka  DATE NOT NULL,
    datum_okonceni DATE NOT NULL,
    nazev          VARCHAR2(32) NOT NULL,
    z_uctu         NUMBER NOT NULL,
    do_uctu        NUMBER NOT NULL,
    id_ucet        NUMBER NOT NULL,
    id_status      NUMBER NOT NULL
);

ALTER TABLE operace ADD CONSTRAINT operace_pk PRIMARY KEY ( id_operace );

CREATE TABLE pobocka (
    id_pobocka     NUMBER NOT NULL,
    nazev          VARCHAR2(32) NOT NULL,
    telefoni_cislo VARCHAR2(20) NOT NULL,
    id_adres       NUMBER NOT NULL,
    id_bank        NUMBER NOT NULL,
    id_status      NUMBER NOT NULL
);

ALTER TABLE pobocka ADD CONSTRAINT pobocka_pk PRIMARY KEY ( id_pobocka );

CREATE TABLE schuzka (
    id_schuzka NUMBER NOT NULL,
    datum      TIMESTAMP  NOT NULL,
    id_pobocka NUMBER NOT NULL,
    id_status  NUMBER NOT NULL
);

ALTER TABLE schuzka ADD CONSTRAINT schuzka_pk PRIMARY KEY ( id_schuzka );

CREATE TABLE status (
    id_status NUMBER NOT NULL,
    popis     VARCHAR2(32) NOT NULL
);

ALTER TABLE status ADD CONSTRAINT status_pk PRIMARY KEY ( id_status );

CREATE TABLE ucet (
    id_ucet        NUMBER NOT NULL,
    cislo_uctu     NUMBER NOT NULL,
    nazev          VARCHAR2(32) NOT NULL,
    id_klient      NUMBER NOT NULL,
    id_bank        NUMBER NOT NULL,
    id_status      NUMBER NOT NULL
   
);

ALTER TABLE ucet ADD CONSTRAINT ucet_pk PRIMARY KEY ( id_ucet );

CREATE TABLE zamestnanec (
    id_zamestnanec NUMBER NOT NULL,
    jmeno          VARCHAR2(32) NOT NULL,
    prijmeni       VARCHAR2(32) NOT NULL,
    mzda           NUMBER NOT NULL,
    funkce         VARCHAR2(32) NOT NULL,
    telefoni_cislo VARCHAR2(20) NOT NULL,
    id_adres       NUMBER NOT NULL,
    id_bank        NUMBER NOT NULL,
    id_status      NUMBER NOT NULL,
    id_pobocka     NUMBER NOT NULL
);

ALTER TABLE zamestnanec ADD CONSTRAINT zamestnanec_pk PRIMARY KEY ( id_zamestnanec );

CREATE TABLE zustatek (
    id_zustatek      NUMBER NOT NULL,
    blokovane_castka NUMBER NOT NULL,
    volna_castka     NUMBER NOT NULL,
    datum           DATE NOT NULL,
    id_ucet          NUMBER NOT NULL
);

ALTER TABLE zustatek ADD CONSTRAINT zustatek_pk PRIMARY KEY ( id_zustatek );

ALTER TABLE bankomat
    ADD CONSTRAINT bankomat_adresa_fk FOREIGN KEY ( id_adres )
        REFERENCES adresa ( id_adres );

ALTER TABLE bankomat
    ADD CONSTRAINT bankomat_bank_fk FOREIGN KEY ( id_bank )
        REFERENCES bank ( id_bank );

ALTER TABLE bankomat
    ADD CONSTRAINT bankomat_status_fk FOREIGN KEY ( id_status )
        REFERENCES status ( id_status );

ALTER TABLE debetni_karta
    ADD CONSTRAINT debetni_karta_karta_fk FOREIGN KEY ( id_karta )
        REFERENCES karta ( id_karta );

ALTER TABLE karta
    ADD CONSTRAINT karta_ucet_fk FOREIGN KEY ( id_ucet )
        REFERENCES ucet ( id_ucet );

ALTER TABLE klient
    ADD CONSTRAINT klient_adresa_fk FOREIGN KEY ( id_adres )
        REFERENCES adresa ( id_adres );

ALTER TABLE klient
    ADD CONSTRAINT klient_bank_fk FOREIGN KEY ( id_bank )
        REFERENCES bank ( id_bank );

ALTER TABLE klient_schuzka
    ADD CONSTRAINT klient_schuzka_klient_fk FOREIGN KEY ( id_klient )
        REFERENCES klient ( id_klient );

ALTER TABLE klient_schuzka
    ADD CONSTRAINT klient_schuzka_schuzka_fk FOREIGN KEY ( id_schuzka )
        REFERENCES schuzka ( id_schuzka );

ALTER TABLE klient
    ADD CONSTRAINT klient_zamestnanec_fk FOREIGN KEY ( id_zamestnanec )
        REFERENCES zamestnanec ( id_zamestnanec );

ALTER TABLE kreditni_karta
    ADD CONSTRAINT kreditni_karta_karta_fk FOREIGN KEY ( id_karta )
        REFERENCES karta ( id_karta );

ALTER TABLE operace
    ADD CONSTRAINT operace_status_fk FOREIGN KEY ( id_status )
        REFERENCES status ( id_status );

ALTER TABLE operace
    ADD CONSTRAINT operace_ucet_fk FOREIGN KEY ( id_ucet )
        REFERENCES ucet ( id_ucet );

ALTER TABLE pobocka
    ADD CONSTRAINT pobocka_adresa_fk FOREIGN KEY ( id_adres )
        REFERENCES adresa ( id_adres );

ALTER TABLE pobocka
    ADD CONSTRAINT pobocka_bank_fk FOREIGN KEY ( id_bank )
        REFERENCES bank ( id_bank );

ALTER TABLE pobocka
    ADD CONSTRAINT pobocka_status_fk FOREIGN KEY ( id_status )
        REFERENCES status ( id_status );

ALTER TABLE schuzka
    ADD CONSTRAINT schuzka_pobocka_fk FOREIGN KEY ( id_pobocka )
        REFERENCES pobocka ( id_pobocka );

ALTER TABLE schuzka
    ADD CONSTRAINT schuzka_status_fk FOREIGN KEY ( id_status )
        REFERENCES status ( id_status );

ALTER TABLE ucet
    ADD CONSTRAINT ucet_bank_fk FOREIGN KEY ( id_bank )
        REFERENCES bank ( id_bank );

ALTER TABLE ucet
    ADD CONSTRAINT ucet_klient_fk FOREIGN KEY ( id_klient )
        REFERENCES klient ( id_klient );

ALTER TABLE ucet
    ADD CONSTRAINT ucet_status_fk FOREIGN KEY ( id_status )
        REFERENCES status ( id_status );



ALTER TABLE zamestnanec
    ADD CONSTRAINT zamestnanec_adresa_fk FOREIGN KEY ( id_adres )
        REFERENCES adresa ( id_adres );

ALTER TABLE zamestnanec
    ADD CONSTRAINT zamestnanec_bank_fk FOREIGN KEY ( id_bank )
        REFERENCES bank ( id_bank );

ALTER TABLE zamestnanec
    ADD CONSTRAINT zamestnanec_pobocka_fk FOREIGN KEY ( id_pobocka )
        REFERENCES pobocka ( id_pobocka );

ALTER TABLE zamestnanec
    ADD CONSTRAINT zamestnanec_status_fk FOREIGN KEY ( id_status )
        REFERENCES status ( id_status );

ALTER TABLE zustatek
    ADD CONSTRAINT zustatek_ucet_fk FOREIGN KEY ( id_ucet )
        REFERENCES ucet ( id_ucet );

CREATE OR REPLACE TRIGGER arc_fkarc_1_debetni_karta BEFORE
    INSERT OR UPDATE OF id_karta ON debetni_karta
    FOR EACH ROW
DECLARE
    d VARCHAR2(1);
BEGIN
    SELECT
        a.typ
    INTO d
    FROM
        karta a
    WHERE
        a.id_karta = :new.id_karta;

    IF ( d IS NULL OR d <> 'D' ) THEN
        raise_application_error(-20223, 'FK debetni_karta_Karta_FK in Table debetni_karta violates Arc constraint on Table Karta - discriminator column typ doesn''t have value ''D'''
        );
    END IF;

EXCEPTION
    WHEN no_data_found THEN
        NULL;
    WHEN OTHERS THEN
        RAISE;
END;
/

CREATE OR REPLACE TRIGGER arc_fkarc_1_kreditni_karta BEFORE
    INSERT OR UPDATE OF id_karta ON kreditni_karta
    FOR EACH ROW
DECLARE
    d VARCHAR2(1);
BEGIN
    SELECT
        a.typ
    INTO d
    FROM
        karta a
    WHERE
        a.id_karta = :new.id_karta;

    IF ( d IS NULL OR d <> 'K' ) THEN
        raise_application_error(-20223, 'FK kreditni_karta_Karta_FK in Table kreditni_karta violates Arc constraint on Table Karta - discriminator column typ doesn''t have value ''K'''
        );
    END IF;

EXCEPTION
    WHEN no_data_found THEN
        NULL;
    WHEN OTHERS THEN
        RAISE;
END;
 
 create sequence adresa_seq minvalue 1 start with 1 increment by 1; 
 

 insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Ceskova','Pardubice','1124','53002','Cesko');

    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Pernerova','Pardubice','145','53002','Cesko');       
    

    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Studentska','Pardubice','201','53009','Cesko');
    

    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Trida Miru','Pardubice','420','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Zavodu miru','Pardubice','1858','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'K Polabinam','Pardubice','1895','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Masarykovo Namesti','Pardubice','1458','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'K Polabinam','Pardubice','200','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'K Polabinam','Pardubice','70','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'K Polabinam','Pardubice','86','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Mladych','Pardubice','24','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Studentska','Pardubice','205','53009','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Studentska','Pardubice','204','53009','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Ceskova','Pardubice','1125','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Jana Palacha','Pardubice','2001','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'V Raji','Pardubice','1762','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Jana Palacha','Pardubice','1444','53002','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Hradecka','Pardubice','470','53009','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'Hradecka','Pardubice','1234','53009','Cesko');
    
    insert into adresa (
    id_adres,ulice,mesto,cislo_popisne,psc,stat) values(
    adresa_seq.nextval,'V Raji','Pardubice','1453','53002','Cesko');
    
    create sequence bank_seq minvalue 1 start with 1 increment by 1 ;
    
    insert into bank (
    id_bank,kod,nazev) values(
    bank_seq.nextval,550,'Moje Banka');
    
    
    create sequence status_seq minvalue 1 start with 1 increment by 1 ;
    insert into status (
    id_status,popis) values(
    status_seq.nextval,'Aktivni');
    
    insert into status (
    id_status,popis) values(
    status_seq.nextval,'Dovolena');
    
    insert into status (
    id_status,popis) values(
    status_seq.nextval,'Provedeno');
    
    insert into status (
    id_status,popis) values(
    status_seq.nextval,'Zruseno');
    
    insert into status (
    id_status,popis) values(
    status_seq.nextval,'Cekani');
    
   insert into status (
    id_status,popis) values(
    status_seq.nextval,'Neaktivni');
    
    
    create sequence pobocka_seq minvalue 1 start with 1 increment by 1 ;
   --drop sequence pobocka_seq;
    
    
    insert into pobocka(id_pobocka,nazev,telefoni_cislo,id_adres,id_bank,id_status) values(
    pobocka_seq.nextval,'Pobocka MojeBanka 1','800100210',1 ,1,1);
    insert into pobocka(id_pobocka,nazev,telefoni_cislo,id_adres,id_bank,id_status) values(
    pobocka_seq.nextval,'Pobocka MojeBanka 2','800100220',2 ,1,1);
    insert into pobocka(id_pobocka,nazev,telefoni_cislo,id_adres,id_bank,id_status) values(
    pobocka_seq.nextval,'Pobocka MojeBanka 3','800100230',3 ,1,1);
    insert into pobocka(id_pobocka,nazev,telefoni_cislo,id_adres,id_bank,id_status) values(
    pobocka_seq.nextval,'Pobocka MojeBanka 4','800100240',4 ,1,1);
    insert into pobocka(id_pobocka,nazev,telefoni_cislo,id_adres,id_bank,id_status) values(
    pobocka_seq.nextval,'Pobocka MojeBanka 5','800100250',5 ,1,1);
    insert into pobocka(id_pobocka,nazev,telefoni_cislo,id_adres,id_bank,id_status) values(
    pobocka_seq.nextval,'Pobocka MojeBanka 6','800100260',6 ,1,6);
    
    
    
    create sequence zamestnanec_seq minvalue 1 start with 1 increment by 1 ; 
    --drop sequence zamestnanec_seq;
    
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Adam','Benz',21000,'Banker','773692693',1,1,1,1);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Eva','Miler',21000,'Banker','773699700',2,1,1,1);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Kent','Feis',22000,'Banker','773400560',3,1,2,1);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Bob','Deniro',20000,'Banker','773490767 ',4,1,1,1);
    
    
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Mark','Robins',23000,'Banker','773200100',5,1,1,2);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Bob','Deniro',25000,'Banker','773310340',6,1,1,2);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Kate','Snou',20000,'Banker','773400560',7,1,2,2);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Scarlet','Williams',23000,'Banker','773901707',8,1,1,2);
    
    
    
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Derak','Helou',17000,'Banker','601345466',8,1,1,3);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Lee','Houpka',19000,'Banker','689777896',10,1,1,3);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Io','Double',20000,'Banker','770433592',11,1,2,3);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Molly','Rubins',14000,'Banker','773801734',11,1,1,3);
    
     insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Toby','Raider',26000,'Banker','900211664',12,1,1,4);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Pop','Kio',15500,'Banker','209456111',12,1,1,4);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Donald','Tramp',20000,'Banker','212354786',13,1,2,4);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Hugh','Ticker',14000,'Banker','904388306',14,1,1,4);
    
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Vasil','Wicnes',26000,'Banker','900211664',12,1,1,5);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Michal','Kio',15500,'Banker','209456111',12,1,1,5);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Qu','Jons',20000,'Banker','212354786',13,1,2,5);
    insert into zamestnanec(id_zamestnanec,jmeno,prijmeni,mzda,funkce,telefoni_cislo,id_adres,id_bank,id_status,id_pobocka)
    values(zamestnanec_seq.nextval,'Anie','Rubins',16500,'Banker','904388306',14,1,1,5);
    
    
    
     create sequence klient_seq minvalue 1 start with 1 increment by 1 ; 
   -- drop sequence klient_seq;
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141750,'Jiri','Parizek','600200101',1,1,1);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141752,'Oleg','Kops','600210102',10,1,1);
    
     insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141754,'Egor','Rarz','600215103',15,1,2);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141755,'Gregory','House','600220104',16,1,2);
    
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141756,'Ada','Req','600230105',17,1,4);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141757,'Toy','House','600235106',6,1,4);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141758,'Qwerz','Angli','600230107',3,1,5);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141759,'Rweq','Gogl','600235108',2,1,5);
    
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141760,'Adark','Uop','600245109',12,1,6);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141761,'Willop','Lopl','600250110',13,1,6);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141762,'Qartz','J','600255111',5,1,8);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141763,'Diamond','Vlob','600260112',8,1,8);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141764,'Fifs','Tyu','600265113',20,1,9);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141765,'Nadya','Golova','600270114',7,1,9);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141766,'Denis','Ufa','600275115',12,1,10);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141767,'Michal','Ekb','600280116',10,1,10);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141768,'Nastya','Parda','600285117',6,1,12);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141769,'Artem','Praga','600290118',5,1,12);
    
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141770,'Marii','Luvs','600295119',4,1,13);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141771,'Vania','Novas','600300120',3,1,13);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141772,'Tor','Yanem','600305121',2,1,14);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141773,'Loki','Barbars','600310122',1,1,14);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141774,'Bag','Kazas','600315123',14,1,16);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141775,'Zeus','Helis','600320124',16,1,16);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141776,'Er','Modls','600325125',15,1,17);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141777,'Uik','Noyer','600330126',17,1,17);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141778,'Mog','Koupli','600335127',17,1,18);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141779,'Klaus','Swhor','600340128',18,1,18);
    
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141780,'Pol','Amers','600345129',19,1,20);
    insert into klient(id_klient,cislo_prukazu,jmeno,prijmeni,telefoni_cislo,id_adres,id_bank,id_zamestnanec)
    values(klient_seq.nextval,141781,'Derb','Jog','600350130',20,1,20);
    
    
    
    
    
    
    create sequence banlomat_seq minvalue 1 start with 1 increment by 1 ;
  -- drop sequence pobocka_seq;
    
    
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 1',1,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 2',2,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 3',3,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 4',4,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 5',5,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 6',6,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 7',7,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 8',8,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 9',9,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 10',10,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 11',11,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 12',12,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 13',13,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 14',14,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 15',15,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 16',16,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 17',17,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 18',18,1,2);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 19',19,1,1);
    insert into bankomat(id_bankomat,nazev,id_adres,id_bank,id_status) values(
    banlomat_seq.nextval,'Bankomat MojeBanka 20',20,1,2);
    
    
    create sequence ucet_seq minvalue 1 start with 1 increment by 1 ;
    --drop sequence ucet_seq;
    
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580201,'Bezny',1,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580202,'Penize',2,1,2);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580203,'Nemam',3,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580204,'Investice',4,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580205,'Money',5,1,2);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580206,'Love it',6,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580207,'Oblibeny',7,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580208,'Binance',8,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580209,'Krypto',9,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580210,'Bezny',10,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580211,'Bezny',11,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580212,'Bezny',12,1,2);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580213,'Bezny',13,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580214,'Dobre',14,1,2);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580215,'Coins',15,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580216,'Eth',16,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580217,'Btc',17,1,2);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580218,'Bezny',18,1,1);
    
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580219,'Bezny',19,1,1);
    
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580220,'Bezny',20,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580221,'Bezny',21,1,2);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580222,'Haha',22,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580223,'Qwerty',23,1,2);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580224,'Name',24,1,2);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580225,'Bezny',25,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580226,'Mzda',26,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580227,'None',27,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580228,'Kde',28,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580229,' Muj',29,1,1);
    
    insert into ucet(id_ucet,cislo_uctu,nazev,id_klient,id_bank,id_status) values(
    ucet_seq.nextval, 6115580230,'Cool',30,1,2);
    
     create sequence schuzka_seq minvalue 1 start with 1 increment by 1 ;
 -- drop sequence schuzka_seq;
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('08.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),1,3 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('08.12.2022 15:00:00', 'DD-MM-YYYY HH24:MI:SS'),1,4 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('10.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),1,5 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('07.12.2022 12:30:00', 'DD-MM-YYYY HH24:MI:SS'),1,3 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('08.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),2,3 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('14.12.2022 16:00:00', 'DD-MM-YYYY HH24:MI:SS'),2,4 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('11.12.2022 12:00:00', 'DD-MM-YYYY HH24:MI:SS'),2,5 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('12.12.2022 11:00:00', 'DD-MM-YYYY HH24:MI:SS'),2,4 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('08.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),3,3 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('08.12.2022 16:00:00', 'DD-MM-YYYY HH24:MI:SS'),3 ,4);
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('10.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),3,4 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('20.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),3,4 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('19.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),4,5 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('20.12.2022 15:00:00', 'DD-MM-YYYY HH24:MI:SS'),4,5 );
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('16.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),4, 5);
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('14.01.2023 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),4, 5);
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('08.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),5, 4);
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('13.12.2022 14:00:00', 'DD-MM-YYYY HH24:MI:SS'),5,5);
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('08.12.2022 15:00:00', 'DD-MM-YYYY HH24:MI:SS'),5,3);
    
    insert into schuzka(id_schuzka,datum,id_pobocka,id_status) values(
    schuzka_seq.nextval,TO_DATE('15.01.2023 16:00:00', 'DD-MM-YYYY HH24:MI:SS'),5,5 );
    
    
    
    create sequence zustatek_seq minvalue 1 start with 1 increment by 1 ;
 -- drop sequence zustatek_seq;
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,300,5600,TO_DATE('08.12.2022', 'DD-MM-YYYY'),1 );
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,459,4210,TO_DATE('08.12.2022', 'DD-MM-YYYY'),2 );
    
     insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,0,1200,TO_DATE('08.12.2022', 'DD-MM-YYYY'),3);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,700,6800,TO_DATE('08.12.2022', 'DD-MM-YYYY'),4);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,12642,30000,TO_DATE('08.12.2022', 'DD-MM-YYYY'),5);
    
   insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,1290,9870,TO_DATE('08.12.2022', 'DD-MM-YYYY'),6);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,250,5144,TO_DATE('08.12.2022', 'DD-MM-YYYY'),7);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,677,1070,TO_DATE('08.12.2022', 'DD-MM-YYYY'),8);
    
   insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,20,908,TO_DATE('08.12.2022', 'DD-MM-YYYY'),9);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,159,8009,TO_DATE('08.12.2022', 'DD-MM-YYYY'),10);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,618,642,TO_DATE('08.12.2022', 'DD-MM-YYYY'),11);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,49,901,TO_DATE('08.12.2022', 'DD-MM-YYYY'),12);
    
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,878,6767,TO_DATE('08.12.2022', 'DD-MM-YYYY'),13);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,903,4556,TO_DATE('08.12.2022', 'DD-MM-YYYY'),14);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,0,61,TO_DATE('08.12.2022', 'DD-MM-YYYY'),15);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,5650,1256,TO_DATE('08.12.2022', 'DD-MM-YYYY'),16);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,4500,777,TO_DATE('08.12.2022', 'DD-MM-YYYY'),17);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,97,6333,TO_DATE('08.12.2022', 'DD-MM-YYYY'),18);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,0,10000,TO_DATE('08.12.2022', 'DD-MM-YYYY'),19);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,150000,1543780,TO_DATE('08.12.2022', 'DD-MM-YYYY'),20);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,0,11249,TO_DATE('08.12.2022', 'DD-MM-YYYY'),21);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,56.9,3770,TO_DATE('08.12.2022', 'DD-MM-YYYY'),22);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,0,4600,TO_DATE('08.12.2022', 'DD-MM-YYYY'),23);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,1349,57000,TO_DATE('08.12.2022', 'DD-MM-YYYY'),24);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,23000,127.8,TO_DATE('08.12.2022', 'DD-MM-YYYY'),25);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,900,10000,TO_DATE('08.12.2022', 'DD-MM-YYYY'),26);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,15,7000,TO_DATE('08.12.2022', 'DD-MM-YYYY'),27);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,115,679,TO_DATE('08.12.2022', 'DD-MM-YYYY'),28);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,670,1300,TO_DATE('08.12.2022', 'DD-MM-YYYY'),29);
    
    insert into zustatek(id_zustatek,blokovane_castka,volna_castka,datum,id_ucet) values(
    zustatek_seq.nextval,2500,25000,TO_DATE('08.12.2022', 'DD-MM-YYYY'),30);
    
    
   

    insert into klient_schuzka(id_klient,id_schuzka) values(
    1,1 );
     insert into klient_schuzka(id_klient,id_schuzka) values(
    2,2 );
    insert into klient_schuzka(id_klient,id_schuzka) values(
   3,3);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    4,4);
    insert into klient_schuzka(id_klient,id_schuzka) values(
    5,5);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    6,6);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    7,7);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    8,8);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    9,9);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    10,10);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    11,11);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    12,12);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    13,13);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    14,14);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    15,15);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    16,16);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    17,17);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    18,18);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
   19,19);
    
    insert into klient_schuzka(id_klient,id_schuzka) values(
    20,20);
    
    create sequence karta_seq minvalue 1 start with 1 increment by 1 ;
 -- drop sequence karta_seq;
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,2956861129631299,'VISA',TO_DATE('12.2026', 'MM-YYYY'),'D',1,'Jiri','Parizek');
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,6172097261075450,'VISA',TO_DATE('12.2026', 'MM-YYYY'),'K',1,'Jiri','Parizek');
    
     insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,5873218204589429,'VISA',TO_DATE('12.2026', 'MM-YYYY'),'D',2,'Oleg','Kops');
    
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,0290677193255978,'VISA',TO_DATE('11.2022', 'MM-YYYY'),'D',2,'Oleg','Kops');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,9151402921891251,'VISA',TO_DATE('09.2023', 'MM-YYYY'),'D',3,'Egor','Rarz');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,3198787229781397,'VISA',TO_DATE('07.2023', 'MM-YYYY'),'K',4,'Gregory','House');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,9484726747643660,'VISA',TO_DATE('07.2023', 'MM-YYYY'),'D',4,'Gregory','House');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,7096040558312143,'VISA',TO_DATE('04.2023', 'MM-YYYY'),'D',5,'Ada','Req');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,9486841045533369,'VISA',TO_DATE('12.2022', 'MM-YYYY'),'D',6,'Toy','House');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,9000621550644898,'VISA',TO_DATE('04.2023', 'MM-YYYY'),'K',6,'Toy','House');
    
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,8228788316050521,'VISA',TO_DATE('04.2023', 'MM-YYYY'),'D',7,'Qwerz','Angli');
    
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,8400273474779821,'VISA',TO_DATE('04.2023', 'MM-YYYY'),'K',8,'Rweq','Gogl');
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,6821624998647683,'VISA',TO_DATE('04.2023', 'MM-YYYY'),'D',8,'Rweq','Gogl');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,3406438909537175,'VISA',TO_DATE('06.2023', 'MM-YYYY'),'D',9,'Adark','Uop');
    
    
     insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,5092328269048627,'VISA',TO_DATE('06.2023', 'MM-YYYY'),'D',10,'Willop','Lopl');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,4606019395510668,'VISA',TO_DATE('06.2023', 'MM-YYYY'),'K',10,'Willop','Lopl');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,6699278989960650,'VISA',TO_DATE('06.2023', 'MM-YYYY'),'D',11,'Qartz','J');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,3010976529124512,'VISA',TO_DATE('06.2023', 'MM-YYYY'),'D',11,'Qartz','J');
    
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,3411425021709534,'VISA',TO_DATE('03.2023', 'MM-YYYY'),'D',12,'Diamond','Vlob');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,2211890901315247,'VISA',TO_DATE('09.2023', 'MM-YYYY'),'D',13,'Fifs','Tyu');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,3352056369284496,'VISA',TO_DATE('09.2023', 'MM-YYYY'),'D',14,'Nadya','Golova');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,2759886461437680,'VISA',TO_DATE('11.2023', 'MM-YYYY'),'D',15,'Denis','Ufa');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,2762643912124342,'VISA',TO_DATE('11.2023', 'MM-YYYY'),'D',16,'Michal','Ekb');
    
     insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,3127983435154213,'VISA',TO_DATE('11.2023', 'MM-YYYY'),'K',16,'Michal','Ekb');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,2340699771443371,'VISA',TO_DATE('08.2025', 'MM-YYYY'),'D',17,'Nastya','Parda');
    
    insert into karta(id_karta,cislo_karty,platebni_system,platnost,typ,id_ucet,jmeno,prijmeni) values(
    karta_seq.nextval,2110485164301241,'VISA',TO_DATE('08.2025', 'MM-YYYY'),'D',18,'Artem','Praga');
    
    
    
    
   
 
    insert into debetni_karta(id_karta,limit) values(1,100000);
    insert into debetni_karta(id_karta,limit) values(3,250000);
    insert into debetni_karta(id_karta,limit) values(4,100000);
    insert into debetni_karta(id_karta,limit) values(5,250000);
    insert into debetni_karta(id_karta,limit) values(7,250000);
    insert into debetni_karta(id_karta,limit) values(8,300000);
    insert into debetni_karta(id_karta,limit) values(9,250000);
    insert into debetni_karta(id_karta,limit) values(11,250000);
    insert into debetni_karta(id_karta,limit) values(13,100000);
    insert into debetni_karta(id_karta,limit) values(14,300000);
    insert into debetni_karta(id_karta,limit) values(15,300000);
    insert into debetni_karta(id_karta,limit) values(17,100000);
    insert into debetni_karta(id_karta,limit) values(18,250000);
    insert into debetni_karta(id_karta,limit) values(19,250000);
    insert into debetni_karta(id_karta,limit) values(20,300000);
    insert into debetni_karta(id_karta,limit) values(21,250000);
    insert into debetni_karta(id_karta,limit) values(22,100000);
    insert into debetni_karta(id_karta,limit) values(23,250000);
    insert into debetni_karta(id_karta,limit) values(25,100000);
    insert into debetni_karta(id_karta,limit) values(26,100000);
    
    
    insert into kreditni_karta(id_karta,uverzovy_limit,bezurocne_obdobi,urokova_sazba,povina_platba_uveru)
    values(2,20000,120,5,3000);
    insert into kreditni_karta(id_karta,uverzovy_limit,bezurocne_obdobi,urokova_sazba,povina_platba_uveru)
    values(6,50000,180,5,5000);
    insert into kreditni_karta(id_karta,uverzovy_limit,bezurocne_obdobi,urokova_sazba,povina_platba_uveru)
    values(10,100000,120,5,4000);
    insert into kreditni_karta(id_karta,uverzovy_limit,bezurocne_obdobi,urokova_sazba,povina_platba_uveru)
    values(12,30000,180,5,3000);
    insert into kreditni_karta(id_karta,uverzovy_limit,bezurocne_obdobi,urokova_sazba,povina_platba_uveru)
    values(16,40000,60,5,1500);
    insert into kreditni_karta(id_karta,uverzovy_limit,bezurocne_obdobi,urokova_sazba,povina_platba_uveru)
    values(24,20000,30,5,3000);
    
    
    
     create sequence operace_seq minvalue 1 start with 1 increment by 1 ;
-- drop sequence operace_seq;
 
    insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,500,TO_DATE('01.12.2022 16:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('02.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'dobit kredit',6115580201,6115580202,1,3);
 
  insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,1400,TO_DATE('08.12.2022 13:30:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('13.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580204,6115580203,4,5);
    
    insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,368,TO_DATE('13.12.2022 12:40:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('15.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580207,6115580206,7,5);
    
    
     insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,100,TO_DATE('08.12.2022 09:40:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('09.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'dobit kredit',6115580230,6115580229,30,3);
    
    insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,24990,TO_DATE('08.12.2022 10:40:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('09.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580202,6115580213,2,3);
    
    insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,589,TO_DATE('08.12.2022 10:40:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('09.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580202,6115580227,2,3);
    
    
    insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,900,TO_DATE('08.12.2022 17:40:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('09.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580217,6115580208,17,3);
    
    
    insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,766,TO_DATE('08.12.2022 17:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('09.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580226,6115580225,26,3);
  --10  
    insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,850,TO_DATE('10.12.2022 8:25:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('15.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'dobit kredit',6115580224,6115580229,24,5);
  --11
   insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
    operace_seq.nextval,900,TO_DATE('09.11.2022 14:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('11.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'dobit kredit',6115580211,6115580215,11,3);
    
    
    --12
insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,655,TO_DATE('09.12.2022 20:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('14.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580206,6115580217,6,5);

insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,877,TO_DATE('11.12.2022 18:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('13.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580207,6115580217,7,5);


insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,1223,TO_DATE('09.12.2022 17:50:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('10.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),'dobit kredit',6115580228,6115580215,28,3);


insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,5433,TO_DATE('09.12.2022 07:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('11.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580230,6115580212,30,5);


insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,546,TO_DATE('09.12.2022 14:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('10.12.2022 13:00:00', 'DD-MM-YYYY HH24:MI:SS'),'dobit kredit',6115580201,6115580209,1,4);


insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,123,TO_DATE('09.12.2022 19:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('10.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'dobit kredit',6115580206,6115580201,6,4);


insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,2340,TO_DATE('09.12.2022 19:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('12.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580208,6115580203,8,4);


insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,1900,TO_DATE('09.12.2022 19:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('12.12.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580205,6115580211,5,5);


insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,10500,TO_DATE('06.12.2022 16:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('07.11.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580222,6115580224,22,3);

insert into operace(id_operace,castka,datum_zacatka,datum_okonceni,nazev,z_uctu,do_uctu,id_ucet,id_status)values(
operace_seq.nextval,10500,TO_DATE('06.12.2022 12:00:00', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('07.11.2022 10:00:00', 'DD-MM-YYYY HH24:MI:SS'),'platba',6115580212,6115580229,12,3);



--A1 :"Seznam zamestnancu   ktery pracuje v pobocke s nazvem pobocka Moje Banka 3 a ",   HK: D1
select z.jmeno, z.prijmeni from zamestnanec z
join pobocka p 
on z.id_pobocka =p.id_pobocka

where p.nazev='Pobocka MojeBanka 3';  


--A2 :"Seznam zamestnancu u kterich status neni aktivni",   HK: D2

select z.jmeno, z.prijmeni  from zamestnanec z
join status  s 
on z.id_status =s.id_status
where s.popis!='Aktivni'; 


--A3 :"Seznam klientu  kterich   vedi Adam Benz",   HK: D3


select k.jmeno,k.prijmeni from klient k
join zamestnanec z 
on k.id_zamestnanec=z.id_zamestnanec
where z.jmeno='Adam';

--A4 :"Seznam klientu  kteri maji libovolnou kartu",   HK: D4
select card.jmeno, card.prijmeni, card.typ from karta card
join ucet u
on u.id_ucet=card.id_ucet;

--A5 :"Seznam bankomatu a pobocek kteri maji jednu adresu  ",   HK: D5
select id_adres, p.nazev as "nazev_poboc", b.nazev as "nazev_bankom" from adresa a
join pobocka p
using(id_adres)
join bankomat b
using(id_adres);


--A6 :"Seznam  bankomatu ktery nepracuje ",   HK: D6

select b.nazev from bankomat b
join status s
on b.id_status=s.id_status
where s.popis='Dovolena';


--A7 :"Seznam  uctu ktery maji zustatek ",   HK: D7

select id_ucet, cislo_uctu,nazev from ucet 
natural join zustatek ;
    
--A8 :"Seznam  uctu ktery maji zustatek ",   HK: D8 
select id_pobocka,nazev, ulice from pobocka, adresa  ;

--A9 :"Seznam  kliebtu a platnost jejich kart",   HK: D9 
select k.id_klient,k.jmeno,k.prijmeni, card.platnost from klient k left outer join karta card 
on card.id_karta=k.id_klient;


--A10 :"Seznam  uctu s volnou catkoj vetsi nez 5000 ",   HK: D10 
select u.id_ucet,u.cislo_uctu, zu.volna_castka from ucet u left outer join zustatek zu 
on u.id_ucet=zu.id_ucet
where zu.volna_castka>5000;

--A11 :"Seznam  klientu kteri maji kartu a taky seznam klientu kteri kartu jeste nema ",   HK: D11
select k.jmeno, k.prijmeni, card.typ from klient k full outer join karta card
on k.jmeno = card.jmeno;

--A12 :"Seznam  zustatku v?t??ch ne? pr?m?rn? z?statek",   HK: D12
select id_zustatek,datum, volna_castka from zustatek 
where volna_castka >(select AVG (volna_castka) from zustatek);


 --A13: „seznam pobo?ek a jejich adresy “, HK: D13
select * from (select a.ulice, a.cislo_popisne, a.psc, p.id_pobocka, p.nazev
from pobocka p
join adresa a
on p.id_adres = a.id_adres);

 --A14: „seznam pobo?ek a jejich adresy “, HK: D14
select z.id_zamestnanec, z.jmeno, z.prijmeni,
(select p.id_pobocka from pobocka p where p.nazev='Pobocka MojeBanka 2')as pobocka from zamestnanec z;


 --A15: „vypisuje maximalni castku  provedene operace “, HK: D15
select o.id_operace,o.castka from operace o
join status s
on o.id_status=s.id_status
where castka=(select max(castka) from operace) ;


 --A16: „seznam aktivniv zamestnancu a vsech klientu  “, HK: D16
select z.id_zamestnanec as id, z.jmeno as jmeno,z.prijmeni as prijmeni from zamestnanec z 
join status s
on z.id_status=s.id_status
where s.popis='Aktivni'
union all select k.id_klient as id ,k.jmeno as jmeno,k.prijmeni as prijmeni from klient k;

--A17: „vypise seznam uctu bez karet “, HK: D17
select id_ucet from ucet minus select id_ucet from karta;


--A18: „vyp??e seznam pobo?ek kter? maj? stejnou adresu s bankomatami“ “, HK: D18
select id_adres from pobocka   intersect select id_adres from bankomat;


--A19: „vyp??e seznam lidi u kterich prijmeni zacina pismenem R“, HK: D19
select concat (concat (prijmeni, ' , '), jmeno)
from zamestnanec 
where prijmeni like 'R%';


--A20: „vyp??e sum  castky provedenech operace od 09.12.2022 “, HK: D20

select sum(o.castka) from operace o
join status s
on s.id_status=o.id_status
where o.datum_zacatka >='09.12.2022' and (s.popis='Provedeno');


--A21: „vyp??e schuzky v ur?it?m datov?m rozmez?  “, HK: D21

select id_schuzka, datum from schuzka
where datum between (to_date('10.12.2022','DD,MM,YY')) and  (to_date('31.12.2022','DD,MM,YY'));


--A22: „ vyp??e minimum, maximum, pr?m?r, sou?et v?ech volnych casek  a jejich po?et  “, HK: D22

select sum(volna_castka) as castka_sum,count(volna_castka) as count_castka,min(volna_castka) as min_chatka ,max(volna_castka) max_chastka,avg(volna_castka) as avg_castka from zustatek;

--A23: „ vyp??e seznam utcu s poctem karret vice nez 1  “, HK: D23
select id_ucet ,count (id_ucet) from karta
group by  id_ucet having count (id_ucet)>1;


--A24: „ vyp??e zamestnancu s jedni pobocce  “, HK: D24

select z.id_zamestnanec,z.jmeno, z.prijmeni, p.id_pobocka from zamestnanec z
join pobocka p
on z.id_pobocka=p.id_pobocka
where p.id_pobocka in (4);


select z.id_zamestnanec,z.jmeno, z.prijmeni, p.id_pobocka from zamestnanec z
left join pobocka p
on z.id_pobocka=p.id_pobocka
where p.id_pobocka = 4;


--A25: „ vyp??e seznam utcu s poctem karret vice nez 1  “, HK: D25

select id_pobocka, count(*) from zamestnanec
where id_pobocka <> 3
group by id_pobocka
having avg(mzda) > 13400
ORDER BY COUNT(*) DESC




--A26: „ pohled ktery zobrazi vsecny ucty s nazvem bezny  “, HK: D26

create or replace view viev_ucet as select nazev from ucet where nazev ='Bezny' or nazev='Penize';


--A27: „ seznam uctu z pohledu jejich nazev zacina na L  “, HK: D27
select nazev
from viev_ucet where nazev like'P%';




--A28: „ seznam uctu z pohledu jejich nazev zacina na L  “, HK: D28


--A29: „ mnena statusu banomata  “, HK: D29
update bankomat set id_status = 5 
where id_status = (select id_status from status where id_status = 2);

--A30: „ odstrani bankomaty s statusem 5 “, HK: D30
delete bankomat where id_status = (select id_status from status where id_status = 5);





