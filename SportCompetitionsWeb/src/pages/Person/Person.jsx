import React, { useEffect, useState } from "react";
import classes from "./Person.module.css";
import Modal from "../../views/Modal/Modal";
import BigLoader from "../../views/BigLoader/BigLoader";
import ButtonImage from "../../views/ButtonImage/ButtonImage";
import useFetching from "../../hooks/useFetching";
import PersonApi from "../../api/personApi";
import { useParams } from "react-router-dom";
import Button from "../../views/Button/Button";
import Input from "../../views/Input/Input";
import ButtonLink from "../../views/ButtonLink/ButtonLink";

const Person = () => {
  const params = useParams();

  const [isOpenModal, isOpenModalChange] = useState(false);
  const [person, personChange] = useState({});
  const [competitions, competitionsChange] = useState([]);
  const [newName, newNameChange] = useState("");
  const [newEmail, newEmailChange] = useState("");
  const [newDate, newDateChange] = useState("");

  const getByIdCallback = async (id) => {
    const response = await PersonApi.getById(id);
    personChange(response.data);
    newNameChange(response.data.name);
    newEmailChange(response.data.email);
    newDateChange(response.data.dateOfBirth);
  };

  const getCompetitionsCallback = async (id) => {
    const response = await PersonApi.getCompetitionsById(id);
    competitionsChange(response.data);
  };

  const changeCallback = async () => {
    const p = {
      name: newName,
      dateOfBirth: newDate,
      email: newEmail,
    };
    await PersonApi.change(params.id, p);
    isOpenModalChange(false);
    const newPerson = {
      name: newName,
      dateOfBirth: newDate,
      email: newEmail,
    };
    personChange(newPerson);
  };

  const [fetchGetById, isLoadingGetById] = useFetching(getByIdCallback);
  const [fetchGetCompetitions, isLoadingGetCompetitions] = useFetching(
    getCompetitionsCallback
  );
  const [fetchChange, isLoadingChange] = useFetching(changeCallback);

  const change = () => {
    isOpenModalChange(true);
  };

  useEffect(() => {
    fetchGetById(params.id);
    fetchGetCompetitions(params.id);
  }, []);

  const Head = () => (
    <div className={classes.header}>
      <div className={classes.logo}>Человек</div>
      <ButtonImage
        src="/images/change.png"
        title="Изменить человека"
        onClick={change}
      />
    </div>
  );

  if (isLoadingGetById || isLoadingGetCompetitions)
    return (
      <div>
        <Head />
        <BigLoader />
      </div>
    );

  return (
    <div>
      <Head />
      <div className={classes.info}>
        <div className={classes.name}>{person.name}</div>
        <div className={classes.params}>
          <div className={classes.date}>{person.dateOfBirth}</div>
          <div>{person.email}</div>
        </div>
      </div>
      <div className={classes.minlogo}>Соревнования</div>
      <table>
        <thead>
          <tr>
            <th>Название</th>
            <th>Дата</th>
            <th>Вид спорта</th>
            <th>Открыть</th>
          </tr>
        </thead>
        <tbody>
          {competitions.map((competition, index) => (
            <tr key={index}>
              <td className={classes.tableName}>{competition.name}</td>
              <td className={classes.tableDate}>{competition.date}</td>
              <td className={classes.tableSport}>{competition.sport.name}</td>
              <td className={classes.tableButton}>
                <ButtonLink text="Открыть" to={`/${competition.id}`} />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Modal active={isOpenModal} setActive={isOpenModalChange}>
        <div className={classes.modalBody}>
          <div className={classes.modalLogo}>Изменить человека</div>
          <Input
            value={newName}
            setValue={newNameChange}
            placeholder="Имя"
            className={classes.inputName}
          />
          <Input
            value={newEmail}
            setValue={newEmailChange}
            placeholder="Email"
            className={classes.inputEmail}
          />
          <input
            type="datetime-local"
            value={newDate}
            onChange={(e) => newDateChange(e.target.value)}
            className={classes.inputDate}
          />
          <Button
            text={"Сохранить"}
            className={classes.addButton}
            isLoading={isLoadingChange}
            onClick={fetchChange}
          />
        </div>
      </Modal>
    </div>
  );
};

export default Person;
