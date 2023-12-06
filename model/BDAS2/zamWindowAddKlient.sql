CREATE OR REPLACE TRIGGER klient_table_trigger
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
/
create or replace TRIGGER adresa_table_trigger
BEFORE INSERT OR UPDATE OR DELETE ON adresa
FOR EACH ROW
DECLARE
    v_user_name VARCHAR2(100);
BEGIN
    SELECT user_name INTO v_user_name FROM (SELECT user_name FROM successful_logins ORDER BY log_time DESC) WHERE ROWNUM = 1;
    IF INSERTING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('ADRESA', 'INSERT', SYSTIMESTAMP, v_user_name, NULL, :NEW.stat|| ' ' || :NEW.mesto || ' ' ||:NEW.ulice|| ' ' ||:NEW.cislo_popisne|| ' ' ||:NEW.psc);

    ELSIF UPDATING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('ADRESA', 'UPDATE', SYSTIMESTAMP, v_user_name, :OLD.stat|| ' ' || :OLD.mesto || ' ' ||:OLD.ulice|| ' ' ||:OLD.cislo_popisne|| ' ' ||:OLD.psc, :NEW.stat|| ' ' || :NEW.mesto || ' ' ||:NEW.ulice|| ' ' ||:NEW.cislo_popisne|| ' ' ||:NEW.psc);

    ELSIF DELETING THEN
        INSERT INTO log_table (tabulka, operace, cas, uzivatel, stara_hodnota, nova_hodnota)
        VALUES ('ADRESA', 'DELETE', SYSTIMESTAMP, v_user_name, :OLD.stat|| ' ' || :OLD.mesto || ' ' ||:OLD.ulice|| ' ' ||:OLD.cislo_popisne|| ' ' ||:OLD.psc, NULL);
    END IF;
END;
/
CREATE OR REPLACE PROCEDURE CreateNewAccount(
  p_klient_id IN NUMBER,
    p_new_account_name IN VARCHAR2
)
IS
    v_account_number NUMBER;
    v_ucet_id NUMBER;
    v_zustatek_id NUMBER;
BEGIN
    -- Вставить новый счет для указанного клиента
    INSERT INTO ucet (id_ucet, cislo_uctu, nazev, klient_id_klient, bank_id_bank, status_id_status)
    VALUES (ucet_id_seq.NEXTVAL, NULL, p_new_account_name, p_klient_id, 1, 5)
    RETURNING id_ucet INTO v_ucet_id;

    -- Получить следующее значение из последовательности для zustatek
    SELECT id_zustatek_seq.nextval INTO v_zustatek_id FROM dual;

    -- Вставить новую строку в таблицу zustatek
    INSERT INTO zustatek (id_zustatek, blokovane_castka, volna_castka, "date", ucet_id_ucet)
    VALUES (v_zustatek_id, 0, 0, SYSDATE, v_ucet_id);

    COMMIT;  -- Commit транзакции
EXCEPTION
    WHEN OTHERS THEN
        -- Обработка исключений по необходимости
        ROLLBACK;  -- Rollback транзакции при ошибке
        RAISE;  -- Пробросить исключение
END CreateNewAccount;
/
