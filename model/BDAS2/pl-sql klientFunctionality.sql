CREATE OR REPLACE PROCEDURE GetKartyForUcet
  (p_ucet_id IN NUMBER, p_cursor OUT SYS_REFCURSOR)
AS
BEGIN
  OPEN p_cursor FOR
    SELECT id_karta, jmeno, prijmeni, cislo_karty, platebni_system, platnost, typ, ucet_id_ucet
    FROM karta
    WHERE ucet_id_ucet = p_ucet_id;
END;
/


CREATE OR REPLACE TRIGGER karta_before_insert
BEFORE INSERT ON karta
FOR EACH ROW
DECLARE
    v_card_number NUMBER;
BEGIN
    -- Generate the random card number using the Luhn algorithm
    v_card_number := FNC_GENERATE_CARD_NUMBER();

    -- Set the generated card number for the new row
    :NEW.cislo_karty := v_card_number;
END karta_before_insert;
/

CREATE OR REPLACE FUNCTION FNC_GENERATE_CARD_NUMBER RETURN NUMBER IS
    v_card_number VARCHAR2(16);
    v_checksum NUMBER;
BEGIN
    -- Generate 15-digit random number as the base card number
    v_card_number := TO_CHAR(TRUNC(DBMS_RANDOM.VALUE(100000000000000, 999999999999999)));

    -- Calculate Luhn algorithm checksum
    v_checksum := 0;
    FOR i IN REVERSE 1..LENGTH(v_card_number) LOOP
        v_checksum := v_checksum + TO_NUMBER(SUBSTR(v_card_number, i, 1) || CASE WHEN MOD(i, 2) = 0 THEN '0' ELSE '2' END);
    END LOOP;

    -- Calculate the Luhn check digit
    v_checksum := MOD(10 - MOD(v_checksum, 10), 10);

    -- Append the Luhn check digit to the card number
    v_card_number := v_card_number || TO_CHAR(v_checksum);

    -- Return the generated card number as a NUMBER
    RETURN TO_NUMBER(v_card_number);
END FNC_GENERATE_CARD_NUMBER;

/
CREATE OR REPLACE FUNCTION GetZustatekForUcet(p_ucet_id IN NUMBER)
RETURN SYS_REFCURSOR
IS
    v_cursor SYS_REFCURSOR;
BEGIN
    OPEN v_cursor FOR
    SELECT id_zustatek, volna_castka, blokovane_castka, "date", ucet_id_ucet
    FROM zustatek
    WHERE ucet_id_ucet = p_ucet_id AND ROWNUM = 1;

    RETURN v_cursor;
END;
/


CREATE OR REPLACE PROCEDURE CreatePlatba(
    p_z_cislo_uctu IN NUMBER,
    p_do_cislo_uctu IN NUMBER,
    p_nazev IN VARCHAR2,
    p_status_id IN NUMBER,
    p_castka IN NUMBER,
    p_ucet_id_ucet IN NUMBER
)
AS
    v_datum_zacatka DATE := SYSTIMESTAMP;
    v_datum_okonceni DATE := SYSTIMESTAMP + INTERVAL '30' MINUTE;
    v_z_ucet_id NUMBER;
    v_do_ucet_id NUMBER;
BEGIN
    -- Get Ucet IDs based on CisloUctu
    SELECT id_ucet INTO v_z_ucet_id FROM Ucet WHERE cislo_uctu = p_z_cislo_uctu;
    SELECT id_ucet INTO v_do_ucet_id FROM Ucet WHERE cislo_uctu = p_do_cislo_uctu;

    -- Insert into Operace using found Ucet IDs
    INSERT INTO operace (
    id_operace,
        castka,
        datum_zacatka,
        datum_okonceni,
        nazev,
        z_uctu,
        do_uctu,
        ucet_id_ucet,
        status_id_status
    ) VALUES (
        id_operace_seq.nextval,
        p_castka,
        v_datum_zacatka,
        v_datum_okonceni,
        p_nazev,
        v_z_ucet_id,
        v_do_ucet_id,
        p_ucet_id_ucet,
        p_status_id
    );

    COMMIT;
END CreatePlatba;
/