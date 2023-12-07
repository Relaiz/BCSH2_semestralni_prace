CREATE OR REPLACE TRIGGER UpdateZustatek
AFTER INSERT ON operace
FOR EACH ROW
DECLARE
BEGIN
  -- Subtract the amount from the source account
  UPDATE zustatek
  SET volna_castka = volna_castka - :new.castka
  WHERE ucet_id_ucet = :new.z_uctu;

  -- Add the amount to the destination account
  UPDATE zustatek
  SET volna_castka = volna_castka + :new.castka
  WHERE ucet_id_ucet = :new.do_uctu;
END;
/

CREATE OR REPLACE PROCEDURE UpdateKlientData(
    p_id_klient IN NUMBER,
    p_jmeno IN VARCHAR2,
    p_prijmeni IN VARCHAR2,
    p_email IN VARCHAR2,
    p_tc in VARCHAR2
)
AS
BEGIN
    UPDATE klient
    SET
        jmeno = p_jmeno,
        prijmeni = p_prijmeni,
        klient_email = p_email,
        telefoni_cislo = p_tc
    WHERE
        id_klient = p_id_klient;

    COMMIT;
END UpdateKlientData;
/

CREATE OR REPLACE PROCEDURE UpdateNazevUctu(
    p_id_ucet IN NUMBER,
    p_nazev IN VARCHAR2
)
AS
BEGIN
    UPDATE ucet
    SET
        nazev = p_nazev
    WHERE
        id_ucet = p_id_ucet;

    COMMIT;
END UpdateNazevUctu;
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
        VALUES ('KLIENT', 'INSERT', SYSTIMESTAMP, v_user_name, NULL, :NEW.jmeno || ' ' ||:NEW.prijmeni);
        
    ELSIF UPDATING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('KLIENT', 'UPDATE', SYSTIMESTAMP, v_user_name, :OLD.jmeno || ' ' || :OLD.prijmeni, :NEW.jmeno || ' ' ||:NEW.prijmeni );

    ELSIF DELETING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('KLIENT', 'DELETE', SYSTIMESTAMP, v_user_name, :OLD.jmeno || ' ' || :OLD.prijmeni, NULL);
    END IF;
END;
