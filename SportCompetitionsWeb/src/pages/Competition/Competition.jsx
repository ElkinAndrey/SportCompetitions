import React, { useEffect, useState } from "react";
import classes from "./Competition.module.css";
import { useParams } from "react-router-dom";
import CompetitionApi from "../../api/competitionApi";
import useFetching from "../../hooks/useFetching";
import ButtonLink from "../../views/ButtonLink/ButtonLink";
import ButtonDelete from "../../views/ButtonDelete/ButtonDelete";
import BigLoader from "../../views/BigLoader/BigLoader";

const Competition = () => {
  const params = useParams();
  const [competition, competitionChange] = useState({});
  const [persons, personsChange] = useState([]);
  const [personsNot, personsNotChange] = useState([]);
  const [deletedId, deletedIdChange] = useState(null);
  const [addId, addIdChange] = useState(null);

  const getByIdCallback = async (id) => {
    const response = await CompetitionApi.getById(id);
    competitionChange(response.data);
  };

  const getPersonsCallback = async (id) => {
    const response = await CompetitionApi.getUsers(id);
    personsChange(response.data);
  };

  const getNotParticipatingPersonsCallback = async (id) => {
    const response = await CompetitionApi.getNotParticipatingUsers(id);
    personsNotChange(response.data);
  };

  const deleteFromCompetitionCallback = async (personId) => {
    await CompetitionApi.deleteFromCompetition(params.id, personId);
    let deletedPerson = persons[persons.findIndex((p) => p.id === personId)];
    personsNotChange([deletedPerson, ...personsNot]);
    const newPersons = persons.filter(
      (competition) => competition.id !== personId
    );
    personsChange([...newPersons]);
    deletedIdChange(null);
  };

  const addToCompetitionCallback = async (personId) => {
    await CompetitionApi.addToCompetition(params.id, personId);

    let addedPerson =
      personsNot[personsNot.findIndex((p) => p.id === personId)];
    personsChange([addedPerson, ...persons]);
    const newPersonsNot = personsNot.filter(
      (competition) => competition.id !== personId
    );
    personsNotChange([...newPersonsNot]);
    addIdChange(null);
  };

  const [fetchGetById, isLoadingGetById] = useFetching(getByIdCallback);
  const [fetchGetUsers, isLoadingGetUsers] = useFetching(getPersonsCallback);
  const [fetchGetNotParticipatingUsers, isLoadingGetNotParticipatingUsers] =
    useFetching(getNotParticipatingPersonsCallback);
  const [fetchDeleteFromCompetition, isLoadingDeleteFromCompetition] =
    useFetching(deleteFromCompetitionCallback);
  const [fetchAddToCompetition, isLoadingAddToCompetition] = useFetching(
    addToCompetitionCallback
  );

  const del = (id) => {
    if (isLoadingDeleteFromCompetition) return;
    deletedIdChange(id);
    fetchDeleteFromCompetition(id);
  };

  const add = (id) => {
    if (isLoadingAddToCompetition) return;
    addIdChange(id);
    fetchAddToCompetition(id);
  };

  useEffect(() => {
    fetchGetById(params.id);
    fetchGetUsers(params.id);
    fetchGetNotParticipatingUsers(params.id);
  }, []);

  if (
    isLoadingGetById ||
    isLoadingGetUsers ||
    isLoadingGetNotParticipatingUsers
  )
    return (
      <div>
        <div className={classes.logo}>Соревнование</div>
        <BigLoader />
      </div>
    );

  return (
    <div>
      <div className={classes.logo}>Соревнование</div>
      <div className={classes.info}>
        <div className={classes.name}>{competition.name}</div>
        <div className={classes.params}>
          <div className={classes.date}>{competition.date}</div>
          <div>{competition.sport?.name}</div>
        </div>
      </div>
      <div className={classes.minlogo}>Участники</div>
      <table className={classes.participants}>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Email</th>
            <th>День рождения</th>
            <th>Открыть</th>
            <th>Изменить</th>
            <th>Исключить</th>
          </tr>
        </thead>
        <tbody>
          {persons.map((person, index) => (
            <tr key={index}>
              <td className={classes.tableName}>{person.name}</td>
              <td className={classes.tableDate}>{person.email}</td>
              <td className={classes.tableSport}>{person.dateOfBirth}</td>
              <td className={classes.tableButton}>
                <ButtonLink text="Открыть" to={`/persons/${person.id}`} />
              </td>
              <td className={classes.tableButton}>
                <ButtonLink
                  text="Изменить"
                  to={`/persons/${person.id}/change`}
                />
              </td>
              <td className={classes.tableButton}>
                <ButtonDelete
                  text="Исключить"
                  onClick={() => del(person.id)}
                  isLoading={
                    isLoadingDeleteFromCompetition && person.id === deletedId
                  }
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className={classes.minlogo}>Остальные люди</div>
      <table>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Email</th>
            <th>День рождения</th>
            <th>Открыть</th>
            <th>Изменить</th>
            <th>Исключить</th>
          </tr>
        </thead>
        <tbody>
          {personsNot.map((person, index) => (
            <tr key={index}>
              <td className={classes.tableName}>{person.name}</td>
              <td className={classes.tableDate}>{person.email}</td>
              <td className={classes.tableSport}>{person.dateOfBirth}</td>
              <td className={classes.tableButton}>
                <ButtonLink text="Открыть" to={`/persons/${person.id}`} />
              </td>
              <td className={classes.tableButton}>
                <ButtonLink
                  text="Изменить"
                  to={`/persons/${person.id}/change`}
                />
              </td>
              <td className={classes.tableButton}>
                <ButtonDelete
                  text="Добавить"
                  onClick={() => add(person.id)}
                  isLoading={isLoadingAddToCompetition && person.id === addId}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Competition;
