create or replace TRIGGER zustatek_table_trigger
BEFORE INSERT OR UPDATE OR DELETE ON zustatek
FOR EACH ROW
DECLARE
    v_user_name VARCHAR2(100);
BEGIN
    SELECT user_name INTO v_user_name FROM (SELECT user_name FROM successful_logins ORDER BY log_time DESC) WHERE ROWNUM = 1;

    IF INSERTING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('ZUSTATEK', 'INSERT', SYSTIMESTAMP, v_user_name, NULL, 'Id zustatku: ' ||:NEW.id_zustatek || ' Patrici ucet: ' ||:NEW.ucet_id_ucet || ' Volne prostredky: ' || :NEW.volna_castka);

    ELSIF UPDATING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('ZUSTATEK', 'UPDATE', SYSTIMESTAMP, v_user_name, 'Id zustatku: ' ||:OLD.id_zustatek || ' Patrici ucet: ' ||:OLD.ucet_id_ucet || ' Volne prostredky: ' || :OLD.volna_castka, 'Id zustatku: ' ||:NEW.id_zustatek || ' Patrici ucet: ' ||:NEW.ucet_id_ucet || ' Volne prostredky: ' || :NEW.volna_castka );

    ELSIF DELETING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('ZUSTATEK', 'DELETE', SYSTIMESTAMP, v_user_name, 'Id zustatku: ' ||:OLD.id_zustatek || ' Patrici ucet: ' ||:OLD.ucet_id_ucet || ' Volne prostredky: ' || :OLD.volna_castka, NULL);
    END IF;
END;
/

create or replace TRIGGER karta_table_trigger
BEFORE INSERT OR UPDATE OR DELETE ON karta
FOR EACH ROW
DECLARE
    v_user_name VARCHAR2(100);
BEGIN
    SELECT user_name INTO v_user_name FROM (SELECT user_name FROM successful_logins ORDER BY log_time DESC) WHERE ROWNUM = 1;

    IF INSERTING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('KARTA', 'INSERT', SYSTIMESTAMP, v_user_name, NULL, 'Uzivatel karty: ' || :NEW.jmeno || ' ' ||:NEW.prijmeni  || ' Cislo karty: ' || :NEW.cislo_karty );

    ELSIF UPDATING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('KARTA', 'UPDATE', SYSTIMESTAMP, v_user_name, 'Uzivatel karty: ' || :OLD.jmeno || ' ' ||:OLD.prijmeni  || ' Cislo karty: ' || :OLD.cislo_karty, 'Uzivatel karty: ' || :NEW.jmeno || ' ' ||:NEW.prijmeni  || 'Cislo karty: ' || :NEW.cislo_karty );

    ELSIF DELETING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('KARTA', 'DELETE', SYSTIMESTAMP, v_user_name, 'Uzivatel karty: ' || :OLD.jmeno || ' ' ||:OLD.prijmeni  || ' Cislo karty: ' || :OLD.cislo_karty, NULL);
    END IF;
END;
/

create or replace TRIGGER operace_table_trigger
BEFORE INSERT OR UPDATE OR DELETE ON operace
FOR EACH ROW
DECLARE
    v_user_name VARCHAR2(100);
BEGIN
    SELECT user_name INTO v_user_name FROM (SELECT user_name FROM successful_logins ORDER BY log_time DESC) WHERE ROWNUM = 1;

    IF INSERTING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('OPERACE', 'INSERT', SYSTIMESTAMP, v_user_name, NULL, 'Z uctu: ' || :NEW.z_uctu || ' Do uctu: ' ||:NEW.do_uctu  || ' Castka: ' || :NEW.castka );

    ELSIF UPDATING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('OPERACE', 'UPDATE', SYSTIMESTAMP, v_user_name, 'Z uctu: ' || :OLD.z_uctu || ' Do uctu: ' ||:OLD.do_uctu  || ' Castka: ' || :OLD.castka, 'Z uctu: ' || :NEW.z_uctu || ' Do uctu: ' ||:NEW.do_uctu  || ' Castka: ' || :NEW.castka);

    ELSIF DELETING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('OPERACE', 'DELETE', SYSTIMESTAMP, v_user_name, 'Z uctu: ' || :OLD.z_uctu || ' Do uctu: ' ||:OLD.do_uctu  || ' Castka: ' || :OLD.castka, NULL);
    END IF;
END;

/

create or replace TRIGGER ucet_table_trigger
BEFORE INSERT OR UPDATE OR DELETE ON ucet
FOR EACH ROW
DECLARE
    v_user_name VARCHAR2(100);
BEGIN
    SELECT user_name INTO v_user_name FROM (SELECT user_name FROM successful_logins ORDER BY log_time DESC) WHERE ROWNUM = 1;

    IF INSERTING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('UCET', 'INSERT', SYSTIMESTAMP, v_user_name, NULL, 'Cislo uctu: ' || :OLD.cislo_uctu || ' Nazev: ' || :NEW.nazev  || ' Id_klientu: ' || :NEW.klient_id_klient);

    ELSIF UPDATING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('UCET', 'UPDATE', SYSTIMESTAMP, v_user_name, 'Cislo uctu: ' || :OLD.cislo_uctu || ' Nazev: ' || :OLD.nazev  || ' Id_klientu: ' || :OLD.klient_id_klient, 'Cislo uctu: ' || :NEW.cislo_uctu || ' Nazev: ' || :NEW.nazev  || ' Id_klientu: ' || :NEW.klient_id_klient);

    ELSIF DELETING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('UCET', 'DELETE', SYSTIMESTAMP, v_user_name, 'Cislo uctu: ' || :OLD.cislo_uctu || ' Nazev: ' || :OLD.nazev  || ' Id_klientu: ' || :OLD.klient_id_klient, NULL);
    END IF;
END;
/

create or replace TRIGGER klient_table_trigger
BEFORE INSERT OR UPDATE OR DELETE ON klient
FOR EACH ROW
DECLARE
    v_user_name VARCHAR2(100);
BEGIN
    SELECT user_name INTO v_user_name FROM (SELECT user_name FROM successful_logins ORDER BY log_time DESC) WHERE ROWNUM = 1;

    IF INSERTING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('KLIENT', 'INSERT', SYSTIMESTAMP, v_user_name, NULL, 'Uzivatel: ' || :NEW.jmeno || ' ' ||:NEW.prijmeni || ' Email uzivatele: ' || :NEW.klient_email || ' Tel: ' || :NEW.telefoni_cislo);

    ELSIF UPDATING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('KLIENT', 'UPDATE', SYSTIMESTAMP, v_user_name, 'Uzivatel: ' || :OLD.jmeno || ' ' ||:OLD.prijmeni || ' Email uzivatele: ' || :OLD.klient_email || ' Tel: ' || :OLD.telefoni_cislo, 'Uzivatel: ' || :NEW.jmeno || ' ' ||:NEW.prijmeni || ' Email uzivatele: ' || :NEW.klient_email || ' Tel: ' || :NEW.telefoni_cislo );

    ELSIF DELETING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('KLIENT', 'DELETE', SYSTIMESTAMP, v_user_name, 'Uzivatel: ' || :OLD.jmeno || ' ' ||:OLD.prijmeni || ' Email uzivatele: ' || :OLD.klient_email || ' Tel: ' || :OLD.telefoni_cislo, NULL);
    END IF;
END;
/

CREATE OR REPLACE VIEW operace_view AS
SELECT
    o.castka,
    o.datum_zacatka,
    o.datum_okonceni,
    o.nazev,
    u1.cislo_uctu AS z_uctu_cislo,
    u2.cislo_uctu AS do_uctu_cislo,
    o.ucet_id_ucet,
    s.popis AS status_popis
FROM
    operace o
    LEFT JOIN ucet u1 ON o.z_uctu = u1.id_ucet
    LEFT JOIN ucet u2 ON o.do_uctu = u2.id_ucet
    LEFT JOIN status s ON o.status_id_status = s.id_status;
