import React from "react";
import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";
import ListGroupItem from "react-bootstrap/ListGroupItem";

const Block = (props) => {
  
  const cardClickHandler = (e) => {
    e.preventDefault();
    console.log(e.target);
    //alert("Click");
  };

  return (
    <div>
      <a style={{ cursor: "pointer" }} onClick={cardClickHandler}>
        <Card style={{ width: "18rem" }}>
          <Card.Header>Block {props.number}</Card.Header>
          <ListGroup  className="list-group-flush">
            <ListGroupItem>Hash: {props.hash}</ListGroupItem>
            <ListGroupItem>Hash of Prev: {props.hashOfPrev}</ListGroupItem>
            <ListGroupItem>Nonce: {props.nonce}</ListGroupItem>
            <ListGroupItem>TimeStamp: {props.timeStamp}</ListGroupItem>
          </ListGroup>
          {/* <Button   variant="primary">Go Details</Button> */}
        </Card>
      </a>
    </div>
  );
};

export default Block;
