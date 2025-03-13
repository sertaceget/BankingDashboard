import { useEffect, useState } from "react";
import axios from "axios";
import { Container, Typography, Button } from "@mui/material";

const DashboardPage = () => {
  const [accounts, setAccounts] = useState([]);

  useEffect(() => {
    const fetchAccounts = async () => {
      const token = localStorage.getItem("token");
      try {
        const response = await axios.get("http://localhost:5184/api/accounts", {
          headers: { Authorization: `Bearer ${token}` },
        });
        setAccounts(response.data);
      } catch (error) {
        console.error("Failed to fetch accounts", error);
      }
    };

    fetchAccounts();
  }, []);

  return (
    <Container>
      <Typography variant="h4">Dashboard</Typography>
      {accounts.map((account: any) => (
        <Typography key={account.id}>{account.accountNumber} - ${account.balance}</Typography>
      ))}
      <Button onClick={() => localStorage.removeItem("token")}>Logout</Button>
    </Container>
  );
};

export default DashboardPage;
