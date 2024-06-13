def BSF(Graph, start):
    for vertex in Graph:
        if start is vertex:
            vertex.v_colour = 'szary'
            vertex.v_d = 0
            vertex.v_previous = float('inf')
        else:
            vertex.v_colour = 'biały'
            vertex.v_d = float('inf')
            vertex.v_previous = None
    Q = collections.deque()
    Q.append(start)
    while len(Q) > 0:
        print(Q)
        vv =  Q.popleft()
        for vertex in Graph[vv]:
            if vertex.v_colour == 'biały':
                vertex.v_colour = 'szary'
                vertex.v_d = vv.v_d + 1
                vertex.v_previous = vv
                Q.append(vertex)
        vv.v_colour = 'czarny'

    return Graph
