import React from 'react';
import Table from 'react-bootstrap/Table';

const Transaction = (props) => {
    return (
        <div>
            <h2>Transactions inside block 1</h2>
            <Table striped bordered hover>
                <thead>
                    <tr>
                    <th>#</th>
                    <th>From</th>
                    <th>To</th>
                    <th>Amount</th>
                    <th>Timestamp</th>
                    <th>Valid?</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                    <td>1</td>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                    </tr>
                    <tr>
                    <td>2</td>
                    <td>Jacob</td>
                    <td>Thornton</td>
                    <td>@fat</td>
                    </tr>
                    <tr>
                    <td>3</td>
                    <td>Larry the Bird</td>
                    <td>@twitter</td>
                    </tr>
                </tbody>
            </Table>
        </div>
    )
}

export default Transaction;
