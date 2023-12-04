CREATE OR REPLACE PROCEDURE GetUctyForKlient(p_klient_id IN NUMBER, p_cursor OUT SYS_REFCURSOR)
AS
BEGIN
  OPEN p_cursor FOR
    SELECT ucet.*
    FROM ucet
    WHERE ucet.klient_id_klient = p_klient_id;
END;
/

INSERT INTO login (zamestnanec_id_zamestnanec, email, heslo, is_admin, klient_id_klient, id_file, id_klient)
SELECT NULL, klient_email, 'B@truha34', 0, 13, NULL, NULL
FROM klient
WHERE id_klient = 13; -- Здесь 1 - это идентификатор записи, которую вы хотите добавить
/

-- matthewharris@gmail.com
-- B@truha34

CREATE OR REPLACE PROCEDURE CreateNewAccount(
    p_klient_id IN NUMBER,
    p_new_account_name IN VARCHAR2
)
IS
    v_account_number NUMBER;
BEGIN
    -- Insert a new account for the specified klient
    INSERT INTO ucet (id_ucet, cislo_uctu, nazev, klient_id_klient, bank_id_bank, status_id_status)
    VALUES (ucet_id_seq.NEXTVAL, NULL, p_new_account_name, p_klient_id, 1, 5)
    RETURNING cislo_uctu INTO v_account_number;

    -- Display or use v_account_number as needed (e.g., display to the user)

    COMMIT;  -- Commit the transaction
EXCEPTION
    WHEN OTHERS THEN
        -- Handle exceptions as needed
        ROLLBACK;  -- Rollback the transaction on error
        RAISE;  -- Propagate the exception
END CreateNewAccount;
/
