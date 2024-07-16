import React, { useState, useEffect } from "react";
import ReactSelect from "react-select";

import { addProcedureUsers } from "../../../api/api";

const PlanProcedureItem = ({ planId, procedure, users, procedureUsers }) => {
  const currentProcedureId = procedure.procedureId;
  const [selectedUsers, setSelectedUsers] = useState(null);
  const [options, setOptions] = useState(users);

  useEffect(() => {
    if (procedureUsers) {
      var filteredProcedures = procedureUsers.filter(
        (x) => x.procedureId === currentProcedureId
      );
      var filteredUsers = filteredProcedures.map((x) => x.userId);
      var procedureMappedUsers = users.filter((x) =>
        filteredUsers.includes(x.value)
      );
      var procedureUnmappedUsers = users.filter(
        (x) => !filteredUsers.includes(x.value)
      );
      setOptions(procedureUnmappedUsers);
      setSelectedUsers(procedureMappedUsers);
    }    
  }, []);

  const handleAssignUserToProcedure = async (e) => {
    try {
      setSelectedUsers(e);
      let usersToAdd;
      if (e?.length === 0) {
        setOptions(users);
        usersToAdd = e;
      } else {
        usersToAdd = e.map((x) => x.value);
      }
      await addProcedureUsers(planId, currentProcedureId, usersToAdd);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="py-2">
      <div>{procedure.procedureTitle}</div>

      <ReactSelect
        className="mt-2"
        placeholder="Select User to Assign"
        isMulti={true}
        options={options}
        value={selectedUsers}
        onChange={(e) => handleAssignUserToProcedure(e)}
      />
    </div>
  );
};

export default PlanProcedureItem;
