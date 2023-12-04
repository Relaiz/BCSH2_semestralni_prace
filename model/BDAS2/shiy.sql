CREATE OR REPLACE VIEW View_Zamestnanec AS
SELECT
    jmeno,
    prijmeni,
    zamestnanec_id_zamestnanec1,
    telefoni_cislo,
    email_zamestnanec,
    mzda,
    prace_pozice_id_pozice
FROM
    zamestnanec;

CREATE OR REPLACE VIEW View_Klient AS
SELECT
    jmeno,
    prijmeni,
    klient_email,
    telefoni_cislo,
    cislo_prukazu
FROM
    klient;
    
    CREATE OR REPLACE VIEW View_Karty_Klientu AS
SELECT
    k.id_karta,
    k.jmeno,
    k.prijmeni,
    k.cislo_karty,
    k.platebni_system,
    k.platnost,
    k.typ,
    k.ucet_id_ucet
FROM
    karta k
JOIN
    klient kl ON k.ucet_id_ucet = kl.id_klient;
    
    CREATE OR REPLACE VIEW View_Ucty_Klientu AS
SELECT
    u.cislo_uctu,
    u.nazev,
    u.status_id_status
FROM
    ucet u
JOIN
    klient kl ON u.klient_id_klient = kl.id_klient;
    
CREATE OR REPLACE FUNCTION GetClientOperationsSum(p_client_id NUMBER) RETURN NUMBER IS
    v_sum NUMBER;
BEGIN
    SELECT SUM(castka)
    INTO v_sum
    FROM operace
    WHERE ucet_id_ucet IN (SELECT id_ucet FROM ucet WHERE klient_id_klient = p_client_id)
        AND EXTRACT(MONTH FROM datum_zacatka) = EXTRACT(MONTH FROM SYSDATE)
        AND EXTRACT(YEAR FROM datum_zacatka) = EXTRACT(YEAR FROM SYSDATE);

    RETURN NVL(v_sum, 0); -- Return 0 if no operations found
END GetClientOperationsSum;
/

CREATE OR REPLACE FUNCTION SendDocument(
    p_id_file IN NUMBER,
    p_nazev_file IN VARCHAR2,
    p_file_location IN VARCHAR2,
    p_prijemce_file IN VARCHAR2,
    p_format_file IN VARCHAR2,
    p_zamestnanec_id_zamestnanec IN NUMBER,
    p_klient_id_klient IN NUMBER,
    p_id_file1 IN NUMBER,
    p_id_klient1 IN NUMBER
)RETURN NUMBER
IS
    v_bfile BFILE;
    v_blob BLOB;
BEGIN
    -- Inserting a PDF file into the BLOB column
    INSERT INTO odesilani_file (id_file, nazev_file, "file", prijemce_file, format_file, zamestnanec_id_zamestnanec, klient_id_klient, id_file1, id_klient1)
    VALUES (p_id_file, p_nazev_file, EMPTY_BLOB(), p_prijemce_file, p_format_file, p_zamestnanec_id_zamestnanec, p_klient_id_klient, p_id_file1, p_id_klient1)
    RETURNING "file" INTO v_blob;

    v_bfile := BFILENAME('YOUR_DIRECTORY', p_file_location); -- Replace with your directory name and file location
    DBMS_LOB.fileopen(v_bfile, DBMS_LOB.file_readonly);
    DBMS_LOB.loadfromfile(v_blob, v_bfile, DBMS_LOB.getlength(v_bfile));
    DBMS_LOB.fileclose(v_bfile);

    COMMIT;

    RETURN 1; -- Success indicator, adjust as needed
EXCEPTION
    WHEN OTHERS THEN
        RETURN 0; -- Failure indicator, adjust as needed
END SendDocument;
/

CREATE OR REPLACE FUNCTION GetCardTransactions(
    p_klient_id IN NUMBER
) RETURN SYS_REFCURSOR AS
    v_result_set SYS_REFCURSOR;

BEGIN
    OPEN v_result_set FOR
        SELECT
            k.id_karta AS card_id,
            u.cislo_uctu AS account_number,
            SUM(o.castka) AS sum_amount,
            MAX(o.datum_zacatka) AS transaction_date
        FROM
            operace o
        JOIN ucet u ON o.z_uctu = u.id_ucet
        JOIN karta k ON u.id_ucet = k.ucet_id_ucet
        WHERE
            u.klient_id_klient = p_klient_id
        GROUP BY
            k.id_karta, u.cislo_uctu;

    RETURN v_result_set;
END GetCardTransactions;
/

CREATE OR REPLACE PROCEDURE GetAvailableFunds(
    p_ucet_id IN NUMBER,
    p_available_funds OUT NUMBER
) AS
    -- Declare variables to hold the result
    v_block_amount NUMBER;
    v_free_amount NUMBER;

BEGIN
    -- Use an implicit cursor to fetch data
    SELECT
        blokovane_castka,
        volna_castka
    INTO
        v_block_amount,
        v_free_amount
    FROM
        zustatek
    WHERE
        ucet_id_ucet = p_ucet_id
        AND "date" = (SELECT MAX("date") FROM zustatek WHERE ucet_id_ucet = p_ucet_id);

    -- Calculate available funds
    p_available_funds := v_free_amount - v_block_amount;
    
    -- Output the result
    DBMS_OUTPUT.PUT_LINE('Available Funds: ' || p_available_funds);
END GetAvailableFunds;
/
CREATE OR REPLACE TRIGGER generate_account_number
BEFORE INSERT ON ucet
FOR EACH ROW
DECLARE
    v_random_number NUMBER;
BEGIN
    -- Generate a random account number (assuming 10-digit account numbers)
    v_random_number := DBMS_RANDOM.VALUE(1000000000, 9999999999);

    -- Assign the generated number to the :NEW.column_name
    :NEW.cislo_uctu := v_random_number;
END generate_account_number;
/
