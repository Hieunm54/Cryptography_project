import React,{useState} from 'react';
import Block from './Block';


const initialBlockData = [
    {
        number: 1,
        hash: 'mh54',
        hashOfPrev:0,
        nonce: 0,
        timeStamp: 1234,
    },
    {
        number: 2,
        hash: 'da123',
        hashOfPrev:'mh54',
        nonce: 243,
        timeStamp: 3344,
    },
    {
        number: 3,
        hash: 'th123',
        hashOfPrev:'da123',
        nonce: 456,
        timeStamp: 11234,
    },{
        number: 4,
        hash: 'asdf4',
        hashOfPrev:'th123',
        nonce: 3425,
        timeStamp: 12334,
    },{
        number: 5,
        hash: 'asdfg',
        hashOfPrev:'asdf4',
        nonce: 3425,
        timeStamp: 12334,
    },
    {
        number: 6,
        hash: 'asd32',
        hashOfPrev:'asdfg',
        nonce: 3425,
        timeStamp: 12334,
    }
]

const BlockList = () => {
    const [blockData,setBlockData] = useState(initialBlockData);

    return (
        <div style={{display: 'flex'}}>
            {blockData.map(block => {

                return <Block 
                key={block.number}
                number={block.number}
                hash={block.hash}
                hashOfPrev={block.hashOfPrev}
                nonce={block.nonce}
                timeStamp={block.timeStamp}

                />
            })}
        </div>
    )
}

export default BlockList
